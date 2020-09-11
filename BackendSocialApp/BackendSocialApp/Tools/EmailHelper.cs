using System.Net;
using System.Net.Mail;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BackendSocialApp.Tools
{
    public class EmailHelper : IEmailHelper
    {
        private string _host;
        private string _port;
        private string _from;
        private string _alias;
        private string _authUserName;
        private string _authUserPassword;
        private string _sendMail;
        private string _bcc;
        private ILogger<EmailHelper> _logger;

        public EmailHelper(IConfiguration configuration, ILogger<EmailHelper> logger)
        {
            _logger = logger;
            var smtpSection = configuration.GetSection("SMTP");
            if (smtpSection != null)
            {
                _host = smtpSection.GetSection("Host").Value;
                _port = smtpSection.GetSection("Port").Value;
                _from = smtpSection.GetSection("From").Value;
                _bcc = smtpSection.GetSection("Bcc").Value;
                _alias = smtpSection.GetSection("Alias").Value;
                _authUserName = smtpSection.GetSection("AuthUserName").Value;
                _authUserPassword = smtpSection.GetSection("AuthUserPassword").Value;
            }

            _sendMail = configuration.GetSection("SendMail").Value;
        }

        public void Send(EmailModel emailModel)
        {
            if(_sendMail == "0")
            {
                _logger.LogInformation("To: " + emailModel.To + "Subject: " + emailModel.Subject + "Subject: " + emailModel.Message);
                return;
            }

            using (SmtpClient client = new SmtpClient(_host, int.Parse(_port)))
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(_from, _alias);
                mailMessage.BodyEncoding = Encoding.UTF8;
                mailMessage.To.Add(emailModel.To);
                mailMessage.Bcc.Add(_bcc);
                mailMessage.Body = emailModel.Message;
                mailMessage.Subject = emailModel.Subject;
                mailMessage.IsBodyHtml = emailModel.IsBodyHtml;

                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(_authUserName, _authUserPassword);

                client.Send(mailMessage);
            }
        }
    }
}
