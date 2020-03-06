using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace BackendSocialApp.Tools
{
    public class MockEmailHelper : IEmailHelper
    {
        public MockEmailHelper(IConfiguration configuration)
        {
        }

        public void Send(EmailModel emailModel)
        {
            Console.WriteLine("To: " + emailModel.To);
            Console.WriteLine("Subject: " + emailModel.Subject);
            Console.WriteLine("IsBodyHtml: " + emailModel.IsBodyHtml);
            Console.WriteLine("Message: " + emailModel.Message);
        }
    }
}
