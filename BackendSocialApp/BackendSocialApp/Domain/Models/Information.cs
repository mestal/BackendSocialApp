using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSocialApp.Domain.Models
{
    public class MainFeed
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string MainPhoto { get; set; }

        public string Info { get; set; }

        public DateTime PublishedDateUtc { get; set; }

        public MainFeedStatus Status { get; set; }
    }

    public enum MainFeedStatus
    {
        Active = 0,
        Deactive = 1
    }

    public class Survey : MainFeed
    {
        public List<SurveyItem> Items { get; set; }

        public List<SurveyResultItem> Results { get; set; }

        public SurveyType SurveyType { get; set; }
    }

    public enum SurveyType
    {
        TrueFalse = 0,
        Personality = 1
    }

    public class SurveyResultItem
    {
        public Guid Id { get; set; }
        public int Point { get; set; }

        public string PicturePath { get; set; }

        public string ResultInformation { get; set; }
        public Survey Survey { get; set; }
    }

    public class SurveyItem
    {
        public Guid Id { get; set; }
        public Survey Survey { get; set; }

        public string PicturePath { get; set; }

        public string Question { get; set; }

        public List<SurveyItemAnswer> Answers { get; set; }

        public int QuestionWeight { get; set; }

        public int Order { get; set; }

        public int MaxSelectableAnswerNumber { get; set; }
    }

    public class SurveyItemAnswer
    {
        public Guid Id { get; set; }

        public SurveyItem SurveyItem { get; set; }

        public string PicturePath { get; set; }

        public string Answer { get; set; }

        public int AnswerWeight { get; set; }

        public int Order { get; set; }
    }

    public class ListNews : MainFeed
    {
        public List<ListNewsItem> Items { get; set; }
    }

    public class ListNewsItem
    {
        public Guid Id { get; set; }
        public ListNews ListNews { get; set; }

        public string PicturePath { get; set; }

        public string Information { get; set; }

        public int Order { get; set; }
    }

    public class News : MainFeed
    {
        public string PicturePath { get; set; }

        public string DetailInformation { get; set; }
    }
}
