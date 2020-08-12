using System;
using System.Collections.Generic;

namespace BackendSocialApp.Domain.Models
{
    public class BusinessException : Exception
    {
        public string Code;
        public List<string> ErrorParams;

        public BusinessException(string code, string message) : this(code, message, null)
        {
        }

        public BusinessException(string code, string message, List<string> errorParams) : base(message)
        {
            Code = code;
            ErrorParams = errorParams;
        }
    }
}