using MediaBrowser.Controller.Library;
using MediaBrowser.Model.Tasks;
using n0tFlix.Addons.Subscriptions.Configuration;
using n0tFlix.Addons.Subscriptions.Email;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace n0tFlix.Addons.Subscriptions.ScheduledTasks
{
    public class SubscriptionsScheduledTask : IScheduledTask, IConfigurableScheduledTask
    {
        private IUserManager UserManager { get; set; }

        public SubscriptionsScheduledTask(IUserManager user)
        {
            UserManager = user;
        }

        private async void ChangeAccountPassword(Subscription subscription, PluginConfiguration config)
        {
            if (config.adminSubscriptionPass != null)
            {
                await UserManager.ChangePassword(UserManager.GetUserById(subscription.Id),
                    config.adminSubscriptionPass);
            }
        }

        private void RemoveUser(Subscription subscription)
        {
            var config = Plugin.Instance.Configuration;
            UserManager.DeleteUser(subscription.user.Id);

            config.subscriptions = RemoveSubscriptionFromSubscriptionList(subscription, config);

            Plugin.Instance.UpdateConfiguration(config);
        }

        private static List<Subscription> RemoveSubscriptionFromSubscriptionList(Subscription subscription, PluginConfiguration config)
        {
            var update = new List<Subscription>();

            foreach (var sub in config.subscriptions)
            {
                if (sub.Id != subscription.Id)
                {
                    update.Add(sub);
                }
            }

            return update;
        }

        public async Task Execute(CancellationToken cancellationToken, IProgress<double> progress)
        {
            var config = Plugin.Instance.Configuration;
            var subscriptionsUpdate = new List<Subscription>();
            var date = DateTime.Now;
            var smtp = new Smtp();
            foreach (var subscription in config.subscriptions)
            {
                var reminderDate = DateTime.Parse(subscription.subscriptionExpire).AddDays(-5);
                var expireDate = DateTime.Parse(subscription.subscriptionExpire);
                var deleteAccountDate = expireDate.AddDays(30);

                if (reminderDate.Year == date.Year &&
                    reminderDate.Day == date.Day &&
                    reminderDate.Month == date.Month &&
                    subscription.reminderSent == false)
                {
                    smtp.SendMail(Smtp.EmailType.PaymentReminder, subscription);

                    subscription.flagForRenewal = true;
                    subscription.reminderSent = true;

                    subscriptionsUpdate.Add(subscription);
                    continue;
                }

                if (expireDate.Year <= date.Year &&
                    expireDate.Day <= date.Day &&
                    expireDate.Month <= date.Month)
                {
                    if (subscription.validSubscription)
                    {
                        subscription.validSubscription = false;
                        smtp.SendMail(Smtp.EmailType.PaymentExpire, subscription);
                        ChangeAccountPassword(subscription, config);
                    }
                    subscriptionsUpdate.Add(subscription);
                    continue;
                }

                if (deleteAccountDate.Year == date.Year &&
                    deleteAccountDate.Day == date.Day &&
                    deleteAccountDate.Month == date.Month)
                {
                    RemoveUser(subscription);
                    continue;
                }

                subscription.flagForRenewal = false;
                subscription.validSubscription = true;
                subscriptionsUpdate.Add(subscription);
            }

            config.subscriptions = subscriptionsUpdate;

            Plugin.Instance.UpdateConfiguration(config);
        }

        public IEnumerable<TaskTriggerInfo> GetDefaultTriggers()
        {
            return new[]
            {
                // Every 24 hours
                new TaskTriggerInfo
                {
                    Type = TaskTriggerInfo.TriggerInterval,
                    IntervalTicks = TimeSpan.FromHours(24).Ticks
                }
            };
        }

        public string Name => "Validate Subscriptions";
        public string Key => "Subscriptions";
        public string Description => "Validate User Subscriptions";
        public string Category => "Subscriptions";
        public bool IsHidden => false;
        public bool IsEnabled => true;
        public bool IsLogged => true;
    }
}