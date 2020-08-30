using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSocialApp.ViewModels
{
    public class FeedViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string MainPhoto { get; set; }
        public string InfoHtml { get; set; }
        public DateTime PublishedDateUtc { get; set; }
        public string FeedType { get; set; }
        public int LikeCount { get; set; }
        public int DislikeCount { get; set; }
        public int CommentCount { get; set; }
    }
}
