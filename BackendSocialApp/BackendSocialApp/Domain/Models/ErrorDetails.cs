using Newtonsoft.Json;
using System.Collections.Generic;

namespace BackendSocialApp.Domain.Models
{
    public class ErrorDetails
    {
        public string Code { get; set; }
        public string Message { get; set; }

        public List<string> ErrorParams { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}