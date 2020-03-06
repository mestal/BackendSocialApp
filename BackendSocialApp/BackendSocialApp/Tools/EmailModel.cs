using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSocialApp.Tools
{
    public class EmailModel
    {
        public string To { get; set; }
        public string Message { get; set; }
        public string Subject { get; set; }
        public bool IsBodyHtml { get; set; }
    }
}
