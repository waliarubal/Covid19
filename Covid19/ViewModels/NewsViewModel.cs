using Covid19.Models;
using Covid19.Services;
using Covid19.Shared.Base;
using Covid19.Shared.Commands;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Covid19.ViewModels
{
    public class NewsViewModel : ViewModelBase
    {
        ICommand _refresh;
        readonly INewsService _newsService;

        public NewsViewModel(INewsService newsService)
        {
            Title = "News Updates";

            _newsService = newsService;

            SearchCommand.Execute(null);
        }

        public ObservableCollection<News> News
        {
            get => Get<ObservableCollection<News>>();
            private set => Set(value);
        }

        public override bool IsCachable => true;

        public ICommand SearchCommand
        {
            get
            {
                if (_refresh == null)
                    _refresh = new RelayCommand<string>(SearchAction) { IsAsynchronous = true };

                return _refresh;
            }
        }

        async void SearchAction(string keywoard)
        {
            IsBusy = true;

            var news = await _newsService.GetFeed(NewsSource.TimesOfIndia);
            News = new ObservableCollection<News>(news);

            IsBusy = false;
        }
    }
}
