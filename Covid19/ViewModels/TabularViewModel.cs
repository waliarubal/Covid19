using Covid19.Models;
using Covid19.Services;
using Covid19.Shared.Base;
using Covid19.Shared.Commands;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Covid19.ViewModels
{
    public class TabularViewModel : ViewModelBase
    {
        ICommand _refresh;
        readonly IJhuCsseService _jhuCsseService;

        public TabularViewModel(IJhuCsseService jhuCsseService)
        {
            _jhuCsseService = jhuCsseService;

            SearchCommand.Execute(null);
        }

        public override bool IsCachable => true;

        public ObservableCollection<Case> Cases
        {
            get => Get<ObservableCollection<Case>>();
            private set => Set(value);
        }

        public string Keywoard
        {
            get => Get<string>();
            set => Set(value);
        }

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

            var cases = await _jhuCsseService.GetCases();

            if (string.IsNullOrEmpty(keywoard))
                Cases = new ObservableCollection<Case>(cases);
            else
            {
                keywoard = keywoard.ToLower();

                var records = new List<Case>();
                foreach(var record in cases)
                {
                    if (record.Country.ToLower().Contains(keywoard))
                        records.Add(record);
                }

                Cases = new ObservableCollection<Case>(records);
            }

            IsBusy = false;
        }
    }
}
