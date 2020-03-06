using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSocialApp.Requests
{
    public class ResetPasswordRequest
    {

        public string Email { get; set; }

        public string NewPassword { get; set; }

        public string NewPassword2 { get; set; }

        public string Token { get; set; }

    }
}
