using MediaBrowser.Model.Dto;
using MediaBrowser.Model.Plugins;
using System;
using System.Collections.Generic;
using System.Text;

namespace n0tFlix.Addons.VideoExtractor.Configuration
{
    public class PluginConfiguration : BasePluginConfiguration
    {
        public List<Subscription> subscriptions { get; set; }
        public HostSmtpInformation hostSmtpInformation { get; set; }
        public string adminSubscriptionPass { get; set; }

        public PluginConfiguration()
        {
        }
    }

    public class HostSmtpInformation
    {
        public string smtpHost { get; set; }
        public string senderAddress { get; set; }
        public int smtpPort { get; set; }
        public string emailUserName { get; set; }
        public string emailPassword { get; set; }
        public string emailDisplayName { get; set; }
    }

    public class Subscription
    {
        public UserDto user { get; set; }
        public string email { get; set; }
        public bool validSubscription { get; set; }

        public string subscriptionStart { get; set; }
        public string subscriptionExpire { get; set; }

        public bool flagForRenewal { get; set; }
        public Guid Id { get; set; }
        public bool reminderSent { get; set; } = false;
    }
}