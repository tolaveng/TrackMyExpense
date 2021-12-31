﻿using Core.Application.Services.IServices;
using Core.Application.Settings;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Core.Application.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSetting emailSetting;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IOptions<EmailSetting> options, ILogger<EmailService> logger)
        {
            emailSetting = options.Value;
            _logger = logger;
        }
        public async Task<bool> SendEmailConfirmationAsync(string email, string name, string confirmEmailLink)
        {
            //TODO: Move to worker
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(emailSetting.SenderEmail, emailSetting.SenderEmail));
            message.To.Add(new MailboxAddress(name, email));

            message.Subject = "Track My Expense Verify your email";

            var builder = new BodyBuilder();
            var textBody = new StringBuilder("Hey there,");
            textBody.AppendLine("Thank you for register your email with TrackMyExpense. To activate your account, please copy and paste the URL below into a web browser.");
            textBody.AppendLine(confirmEmailLink);
            textBody.AppendLine("Please ignore this message if you did not associate this email address.");
            
            builder.TextBody = textBody.ToString();

            var htmlBody = new StringBuilder("<span style='font-size: 36px; color: #0000ff'>TrackMyExpense</span><br/><br/>");
            htmlBody.AppendLine("Hey there,<br/>");
            htmlBody.AppendLine("Thank you for register your email with TrackMyExpense. To activate your account, please verify your email by clicking the link below:<br/>");
            htmlBody.AppendLine($"<a href=\"{confirmEmailLink}\" target=\"_blank\">Verify my email address</a><br/><br/>");
            htmlBody.AppendLine("If you experience any issues with the link above, copy and paste the URL below into a web browser.<br/><br/>");
            htmlBody.AppendLine($"{confirmEmailLink}<br/><br/>");
            htmlBody.AppendLine("Please ignore this message if you did not associate this email address.<br/>");
            builder.HtmlBody = htmlBody.ToString();

            message.Body = builder.ToMessageBody();
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(emailSetting.Host, emailSetting.Port, emailSetting.SSL);
                    client.Authenticate(emailSetting.User, emailSetting.Password);
                    client.Send(message);
                    client.Disconnect(true);

                } catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    _logger.LogError(ex.StackTrace);
                    return false;
                }
            }

            return true;
        }
    }
}
