using Core.Application.Services.IServices;
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

        private const string logoHtml = @"
            <span style=""font-family:'Times New Roman','Times-Roman',serif; font-size:32px"">
            <span style='color:#00796B'>T</span>
            <span style='color:#00e8ea'>r</span>
            <span style='color:#02befd'>a</span>
            <span style='color:#059afa'>c</span>
            <span style='color:#056afa'>k</span>
            <span style='color:#0538fa'>M</span>
            <span style='color:#5303fc'>y</span>
            <span style='color:#7a04fb'>E</span>
            <span style='color:#a302fd'>x</span>
            <span style='color:#ca05fa'>p</span>
            <span style='color:#eb04fb'>e</span>
            <span style='color:#fb04d7'>n</span>
            <span style='color:#fa0596'>s</span>
            <span style='color:#fc0351'>e</span>
            </span>
        ";

        public EmailService(IOptions<EmailSetting> options, ILogger<EmailService> logger)
        {
            emailSetting = options.Value;
            _logger = logger;
        }
        public bool SendNoReplyEmailAsync(string receiverEmail, string receiverName,
            string subject, string textBody, string htmlBody)
        {
            //TODO: Move to worker
            var message = new MimeMessage();
            message.Subject = subject;
            message.From.Add(new MailboxAddress(emailSetting.SenderEmail, emailSetting.SenderEmail));
            message.To.Add(new MailboxAddress(receiverName, receiverEmail));

            var builder = new BodyBuilder();
            builder.TextBody = textBody;
            builder.HtmlBody = htmlBody;
            message.Body = builder.ToMessageBody();

            try
            {
                using (var client = new SmtpClient())
                {
                    client.Connect(emailSetting.Host, emailSetting.Port, emailSetting.SSL);
                    client.Authenticate(emailSetting.User, emailSetting.Password);
                    client.Send(message);
                    client.Disconnect(true);
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                return false;
            }
        }

        public bool SendEmailConfirmation(string receiverEmail, string receiverName, string confirmEmailLink)
        {
            var subject = "Track My Expense - Verify your email";
            var textBody = new StringBuilder("Hey there,");
            textBody.AppendLine("Thank you for register your email with TrackMyExpense. To activate your account, please copy and paste the URL below into a web browser.");
            textBody.AppendLine(confirmEmailLink);
            textBody.AppendLine("Please ignore this message if you did not associate this email address.");
            textBody.AppendLine("\nKind regards,\nTrack My Expense Teams");

            var htmlBody = new StringBuilder($"{logoHtml}<br/><br/>");
            htmlBody.AppendLine("Hey there,<br/>");
            htmlBody.AppendLine("Thank you for register your email with TrackMyExpense. To activate your account, please verify your email by clicking the link below:<br/>");
            htmlBody.AppendLine($"<a href=\"{confirmEmailLink}\" target=\"_blank\">Verify my email address</a><br/><br/>");
            htmlBody.AppendLine("If you experience any issues with the link above, copy and paste the URL below into a web browser.<br/><br/>");
            htmlBody.AppendLine($"{confirmEmailLink}<br/><br/>");
            htmlBody.AppendLine("Please ignore this message if you did not associate this email address.<br/>");
            htmlBody.AppendLine("<br/>Kind regards,<br/>Track My Expense Teams");

            return SendNoReplyEmailAsync(receiverEmail, receiverName, subject, textBody.ToString(), htmlBody.ToString());
        }

        public bool SendPasswordReset(string receiverEmail, string receiverName, string confirmEmailLink)
        {
            var subject = "Track My Expense - Reset Password";
            var textBody = new StringBuilder("Hey there,");
            textBody.AppendLine("To reset your password, please click on the link below within 24 hours.");
            textBody.AppendLine(confirmEmailLink);
            textBody.AppendLine("Please ignore this message if you did not request to reset your password.");
            textBody.AppendLine("\nKind regards,\nTrack My Expense Teams");

            var htmlBody = new StringBuilder($"{logoHtml}<br/><br/>");
            htmlBody.AppendLine("Hey there,<br/>");
            htmlBody.AppendLine("To reset your password, please click on the link below within 24 hours.<br/>");
            htmlBody.AppendLine($"<a href=\"{confirmEmailLink}\" target=\"_blank\">Reset my password</a><br/><br/>");
            htmlBody.AppendLine("If you experience any issues with the link above, copy and paste the URL below into a web browser.<br/><br/>");
            htmlBody.AppendLine($"{confirmEmailLink}<br/><br/>");
            htmlBody.AppendLine("Please ignore this message if you did not request to reset your password.<br/>");
            htmlBody.AppendLine("<br/>Kind regards,<br/>Track My Expense Teams");

            return SendNoReplyEmailAsync(receiverEmail, receiverName, subject, textBody.ToString(), htmlBody.ToString());
        }
    }
}
