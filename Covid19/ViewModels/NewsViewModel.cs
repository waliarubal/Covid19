using Covid19.Models;
using Covid19.Services;
using Covid19.Shared.Base;
using Covid19.Shared.Commands;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Covid19.ViewModels
{
    public class NewsViewModel : ViewModelBase
    {
        ICommand _refresh;
        readonly INewsService _newsService;
        readonly ISettingsService _settingsService;

        public NewsViewModel(INewsService newsService, ISettingsService settingsService)
        {
            Title = "News Updates";

            _newsService = newsService;
            _settingsService = settingsService;

            RefreshCommand.Execute(null);
        }

        public ObservableCollection<News> News
        {
            get => Get<ObservableCollection<News>>();
            private set => Set(value);
        }

        public override bool IsCachable => true;

        public ICommand RefreshCommand
        {
            get
            {
                if (_refresh == null)
                    _refresh = new RelayCommand<string>(RefreshAction) { IsAsynchronous = true };

                return _refresh;
            }
        }

        async void RefreshAction(string keywoard)
        {
            IsBusy = true;

            var news = new List<News>();
            IEnumerable<News> newsFeed;

            if (_settingsService.Get<bool>(nameof(SettingsViewModel.IsAlJazeera)))
            {
                newsFeed = await _newsService.GetFeed(NewsSource.AlJazeera);
                news.AddRange(newsFeed);
            }

            if (_settingsService.Get<bool>(nameof(SettingsViewModel.IsBbc)))
            {
                newsFeed = await _newsService.GetFeed(NewsSource.BBCNews);
                news.AddRange(newsFeed);
            }

            if (_settingsService.Get<bool>(nameof(SettingsViewModel.IsCnn)))
            {
                newsFeed = await _newsService.GetFeed(NewsSource.CNN);
                news.AddRange(newsFeed);
            }

            if (_settingsService.Get<bool>(nameof(SettingsViewModel.IsTheNewYorkTimes)))
            {
                newsFeed = await _newsService.GetFeed(NewsSource.TheNewYorkTimes);
                news.AddRange(newsFeed);
            }

            if (_settingsService.Get<bool>(nameof(SettingsViewModel.IsTimesOfIndia)))
            {
                newsFeed = await _newsService.GetFeed(NewsSource.TimesOfIndia);
                news.AddRange(newsFeed);
            }

            if (_settingsService.Get<bool>(nameof(SettingsViewModel.IsYahooNews)))
            {
                newsFeed = await _newsService.GetFeed(NewsSource.YahooNews);
                news.AddRange(newsFeed);
            }

            News = new ObservableCollection<News>(news);

            IsBusy = false;
        }
    }
}
