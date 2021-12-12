using MediaBrowser.Controller.Library;
using MediaBrowser.Common.Configuration;
using MediaBrowser.Common.Plugins;
using MediaBrowser.Model.Serialization;
using System.Net.Http;
using MediaBrowser.Model.Users;
using n0tFlix.Addons.Subscriptions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace n0tFlix.Addons.Subscriptions.API
{
    public class CsvInfo
    {
        public string Path { get; set; }

        //Columns - Follow this in the CSV
        public string UserName { get; set; }  //0

        public string AccountId { get; set; }  //1
        public string Password { get; set; }  //2
        public string ExpiryDate { get; set; }  //3
        public string EmailAddress { get; set; }  //4
    }

    public class SubscriptionRemovalData
    {
        public string Id { get; set; }
    }

    [ApiController]
    public class SubscriptionsService : ControllerBase
    {
        private IJsonSerializer JsonSerializer { get; set; }
        private IUserManager UserManager { get; set; }

        public SubscriptionsService(IJsonSerializer json, IUserManager user)
        {
            
            JsonSerializer = json;
            UserManager = user;
        }

        [Route("SubscriptionsService/AddCsvInfo/{result:CsvInfo}")]
        public string Get(CsvInfo result)
        {
            try
            {
                List<CsvInfo> csv = new List<CsvInfo>();

                using (var reader = new StreamReader(result.Path))
                {
                    var line = reader.ReadLine();

                    while (!reader.EndOfStream)
                    {
                        line = reader.ReadLine();
                        csv.Add(new CsvInfo()
                        {
                            AccountId = line.Split(',')[1],
                            ExpiryDate = line.Split(',')[3],
                            Password = line.Split(',')[2],
                            UserName = line.Split(',')[0],
                            EmailAddress = line.Split(',')[4]
                        });
                    }
                }

                var config = Plugin.Instance.Configuration;
                foreach (var account in csv)
                {
                    if (config.subscriptions.All(subscription => subscription.Id.ToString() != account.AccountId))
                    {
                        var expireDateYear = account.ExpiryDate.Split('-')[2];
                        var expireDateMonth = account.ExpiryDate.Split('-')[1];
                        var expireDateDay = account.ExpiryDate.Split('-')[0];

                        var expireDate = DateTime.Parse(expireDateMonth + "/" + expireDateDay + "/" + expireDateYear);

                        var newUser = UserManager.CreateUserAsync(account.UserName).Result;
                        {
                            newUser.EnableUserPreferenceAccess = false;
                            newUser.EnableUserPreferenceAccess = true;
                            newUser.DisplayMissingEpisodes = true;
                            newUser.EnableNextEpisodeAutoPlay = true;
                            newUser.LoginAttemptsBeforeLockout = 5;
                            newUser.RememberAudioSelections = true;
                            newUser.Id = Guid.NewGuid();
                            newUser.RememberSubtitleSelections = true;
                            newUser.SetPermission(Jellyfin.Data.Enums.PermissionKind.IsHidden, true);
                            newUser.SetPermission(Jellyfin.Data.Enums.PermissionKind.EnableRemoteControlOfOtherUsers, false);
                            newUser.SetPermission(Jellyfin.Data.Enums.PermissionKind.EnablePlaybackRemuxing, true);
                            newUser.SetPermission(Jellyfin.Data.Enums.PermissionKind.EnableMediaPlayback, true);
                            newUser.SetPermission(Jellyfin.Data.Enums.PermissionKind.EnableContentDownloading, true);
                            newUser.SetPermission(Jellyfin.Data.Enums.PermissionKind.EnableLiveTvAccess, true);
                            newUser.SetPermission(Jellyfin.Data.Enums.PermissionKind.IsAdministrator, false);
                            newUser.SetPermission(Jellyfin.Data.Enums.PermissionKind.EnableVideoPlaybackTranscoding, true);
                            newUser.SetPermission(Jellyfin.Data.Enums.PermissionKind.EnableMediaPlayback, true);
                            newUser.SetPermission(Jellyfin.Data.Enums.PermissionKind.EnableSharedDeviceControl, true);
                            newUser.RemoteClientBitrateLimit = 10;
                        };

                        UserManager.ChangePassword(newUser, account.Password);

                        config.subscriptions.Add(new Subscription()
                        {
                            email = account.EmailAddress,
                            flagForRenewal = DateTime.Now > expireDate.AddDays(-5),
                            Id = newUser.Id,
                            reminderSent = false,
                            subscriptionExpire = expireDate.Year + "/" + expireDate.Month + "/" + expireDate.Day,
                            subscriptionStart = "",
                            user = UserManager.GetUserDto(newUser),
                            validSubscription = DateTime.Now.Date < expireDate.Date
                        });
                    }
                }

                Plugin.Instance.UpdateConfiguration(config);
            }
            catch (Exception ex)
            {
                return JsonSerializer.SerializeToString(new ResponseInfo()
                {
                    response = "CSVError - " + ex.Message
                });
            }

            return JsonSerializer.SerializeToString(new ResponseInfo()
            {
                response = "OK"
            });
        }
        
        [Route("SubscriptionsService/RemoveSubscriptionAndUser/{result:SubscriptionRemovalData}")]
        public string Get(SubscriptionRemovalData result)
        {
            var config = Plugin.Instance.Configuration;
            UserManager.DeleteUser(Guid.Parse(result.Id));

            var update = new List<Subscription>();

            foreach (var sub in config.subscriptions)
            {
                if (sub.Id.ToString() != result.Id)
                {
                    update.Add(sub);
                }
            }

            config.subscriptions = update;

            Plugin.Instance.UpdateConfiguration(config);

            return JsonSerializer.SerializeToString(new ResponseInfo() { response = "OK" });
        }

        private class ResponseInfo
        {
            public string response { get; set; }
        }
    }
}