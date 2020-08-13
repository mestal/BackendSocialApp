using System.Net;
using System.Net.Mail;
using System.Text;
using Microsoft.Extensions.Configuration;

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

        public EmailHelper(IConfiguration configuration)
        {
            var smtpSection = configuration.GetSection("SMTP");
            if (smtpSection != null)
            {
                _host = smtpSection.GetSection("Host").Value;
                _port = smtpSection.GetSection("Port").Value;
                _from = smtpSection.GetSection("From").Value;
                _alias = smtpSection.GetSection("Alias").Value;
                _authUserName = smtpSection.GetSection("AuthUserName").Value;
                _authUserPassword = smtpSection.GetSection("AuthUserPassword").Value;
                _sendMail = smtpSection.GetSection("SendMail").Value;
            }
        }

        public void Send(EmailModel emailModel)
        {
            if(_sendMail == "0")
            {
                return;
            }

            using (SmtpClient client = new SmtpClient(_host, int.Parse(_port)))
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(_from, _alias);
                mailMessage.BodyEncoding = Encoding.UTF8;
                mailMessage.To.Add(emailModel.To);
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
