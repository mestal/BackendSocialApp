﻿using BackendSocialApp.Domain.Models;
using System;
using System.Collections.Generic;

namespace BackendSocialApp.ViewModels
{
    public class SurveyViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string MainPhoto { get; set; }
        public string InfoHtml { get; set; }
        public DateTime PublishedDateUtc { get; set; }
        public string SurveyType { get; set; }
        public List<SurveyItemViewModel> Items { get; set; }
        public List<SurveyResultItemViewModel> Results { get; set; }
        public int LikeCount { get; set; }
        public int DislikeCount { get; set; }
        public int CommentCount { get; set; }
        public int LikedType { get; set; }
    }

    public class SurveyItemViewModel
    {
        public Guid Id { get; set; }

        public string PicturePath { get; set; }

        public string VideoPath { get; set; }

        public string Question { get; set; }

        public List<SurveyItemAnswerViewModel> Answers { get; set; }

        public int QuestionWeight { get; set; }

        public int Order { get; set; }

        public int MaxSelectableAnswerNumber { get; set; }
    }

    public class SurveyItemAnswerViewModel
    {
        public Guid Id { get; set; }

        public string PicturePath { get; set; }

        public string Answer { get; set; }

        public int AnswerWeight { get; set; }

        public int Order { get; set; }
    }

    public class SurveyResultItemViewModel
    {
        public Guid Id { get; set; }
        public int Point { get; set; }

        public string PicturePath { get; set; }
        public string Title { get; set; }

        public string ResultInformation { get; set; }
    }

    public class NewsViewModel : FeedViewModel
    {
        public List<NewsItemViewModel> Items { get; set; }
    }

    public class NewsItemViewModel
    {
        public Guid Id { get; set; }

        public string PicturePath { get; set; }

        public string VideoPath { get; set; }

        public string Information { get; set; }

        public string Title { get; set; }

        public int Order { get; set; }
    }

    public class CommentViewModel
    {
        public Guid Id { get; set; }

        public string Comment { get; set; }

        public UserViewModel User  { get;set; }

        public DateTime CreateDate { get; set; }

    }
}
