using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSocialApp.Requests
{
    public class SubmitCommentRequest
    {
        public string Comment { get; set; }

        public Guid FeedId { get; set; }
    }
}
