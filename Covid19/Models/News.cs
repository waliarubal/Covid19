using Covid19.Shared.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Covid19.Models
{
    public enum NewsSource : byte
    {
        REDDITWorldNews,
        BBCNews,
        TheNewYorkTimes,
        AlJazeera,
        YahooNews,
        CNN,
        TimesOfIndia
    }

    public class News: ModelBase
    {
        public News(NewsSource source, string title)
        {
            Source = source;
            Title = title;
        }

        public NewsSource Source
        {
            get => Get<NewsSource>();
            private set => Set(value);
        }

        public string Title
        {
            get => Get<string>();
            set => Set(value);
        }

        public string Description
        {
            get => Get<string>();
            set => Set(value);
        }

        public string Content
        {
            get => Get<string>();
            set => Set(value);
        }

        public string Author
        {
            get => Get<string>();
            set => Set(value);
        }

        public DateTime? PublishingDate
        {
            get => Get<DateTime?>();
            set => Set(value);
        }
    }
}
