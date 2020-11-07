using n0tFlix.Addons.Subscriptions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace n0tFlix.Addons.Subscriptions.Email
{
    public class Smtp
    {
        public enum EmailType
        {
            PaymentReminder = 0,//=> "Upcoming Payment Reminder";
            PaymentExpire = 1, //> "Payment Overdue";
            PaymentReceived = 2 //> "Payment Received";
        }

        private SmtpClient client { get; set; }

        public void SendMail(EmailType emailType, Subscription subscription)
        {
            var config = Plugin.Instance.Configuration;

            if (config.hostSmtpInformation == null) return;

            var host = config.hostSmtpInformation.smtpHost;
            var port = config.hostSmtpInformation.smtpPort;
            var userName = config.hostSmtpInformation.emailUserName;
            var password = config.hostSmtpInformation.emailPassword;

            client = new SmtpClient()
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(userName, password),
                EnableSsl = true,
                Host = host,
                Port = port,
                DeliveryMethod = SmtpDeliveryMethod.Network
            };
            switch (emailType)
            {
                case EmailType.PaymentReminder:
                    client.Send(CreateReminderMessage(config, subscription));
                    break;

                case EmailType.PaymentExpire:
                    client.Send(CreateExpireMessage(config, subscription));
                    break;
            }

            client.SendCompleted += emailSendComplete;
        }

        private static MailMessage CreateExpireMessage(PluginConfiguration config, Subscription subscription)
        {
            var expireYear = DateTime.Parse(subscription.subscriptionExpire).Year;
            var expireMonth = DateTime.Parse(subscription.subscriptionExpire).Month;
            var expireDay = DateTime.Parse(subscription.subscriptionExpire).Day;

            var html = "";

            html += @"<div style='background-color:#303030; height:400px'>";
            html += @"<div style='margin:2em'>";
            html += @"<h3 style='color:white;font-size:1.17em;padding-top:2em'>Hello " + subscription.user.Name + "</h3>";
            html += @"<h3 style='color:white; font-size:1.17em'>We're sorry to see you go. Your subscription to " +
                    config.hostSmtpInformation.emailDisplayName + " expired on:</h3>";
            html += @"<h3 style='color:white; font-size:1.17em'>" + expireDay + @"/" + expireMonth + @"/" + expireYear + @"</h3>";
            html += @"<h3 style='color:white'>Please contact us in the future if you wish to enable your subscription.</h3>";
            html += @"<div>";
            html += @"</div>";

            return new MailMessage(new MailAddress(config.hostSmtpInformation.senderAddress), new MailAddress(subscription.email))
            {
                IsBodyHtml = true,
                Subject = "Account Expired",
                From = new MailAddress(config.hostSmtpInformation.senderAddress,
                    config.hostSmtpInformation.emailDisplayName),
                Body = html,
            };
        }

        private static MailMessage CreateReminderMessage(PluginConfiguration config, Subscription subscription)
        {
            var expireYear = DateTime.Parse(subscription.subscriptionExpire).Year;
            var expireMonth = DateTime.Parse(subscription.subscriptionExpire).Month;
            var expireDay = DateTime.Parse(subscription.subscriptionExpire).Day;

            var html = "";

            html += @"<div style='background-color:#303030; height: 400px'>";
            html += @"<div style='margin:2em'>";
            html += @"<h3 style='color:white; font-size:1.17em; padding-top:2em'>Hello " + subscription.user.Name + "</h3>";
            html += @"<p style='color:white; font-size:1.17em'>This is a simple reminder that your subscription to " +
                    config.hostSmtpInformation.emailDisplayName + " will expire on:</p>";
            html += @"<h3 style='color:white; font-size:1.17em'>" + expireDay + @"/" + expireMonth + @"/" + expireYear + @"</h3>";
            html += @"<div>";
            html += @"</div>";

            return new MailMessage(new MailAddress(config.hostSmtpInformation.senderAddress), new MailAddress(subscription.email))
            {
                IsBodyHtml = true,
                Subject = "Upcoming Payment Reminder",
                From = new MailAddress(config.hostSmtpInformation.senderAddress,
                    config.hostSmtpInformation.emailDisplayName),
                Body = html
            };
        }

        private void emailSendComplete(object sender, AsyncCompletedEventArgs e)
        {
        }
    }
}