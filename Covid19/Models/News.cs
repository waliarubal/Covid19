using Covid19.Shared.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Covid19.Models
{
    public enum NewsSource : byte
    {
        BBCNews,
        TheNewYorkTimes,
        AlJazeera,
        YahooNews,
        CNN,
        TimesOfIndia
    }

    public class News : ModelBase
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
            set
            {
                Set(value);
                RaisePropertyChanged(nameof(IsHavingAuthor));
            }
        }

        public bool IsHavingAuthor => !string.IsNullOrWhiteSpace(Author);

        public DateTime? PublishingDate
        {
            get => Get<DateTime?>();
            set
            {
                Set(value);
                RaisePropertyChanged(nameof(IsHavingPublisingDate));
            }
        }

        public bool IsHavingPublisingDate => PublishingDate.HasValue;
    }
}
