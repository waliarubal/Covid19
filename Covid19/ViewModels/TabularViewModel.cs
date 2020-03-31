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

        public TabularViewModel(IJhuCsseService jhuCsseService, ISettingsService settingsService)
        {
            _jhuCsseService = jhuCsseService;
            Title = "Tabular View";
            IsTotal = settingsService.Get<bool>(nameof(SettingsViewModel.IsTotal));

            SearchCommand.Execute(Keywoard);
        }

        public override bool IsCachable => true;

        public bool IsTotal
        {
            get => Get<bool>();
            private set => Set(value);
        }

        public CaseCollection Cases
        {
            get => Get<CaseCollection>();
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
            Cases = await _jhuCsseService.GetCases(keywoard);
            IsBusy = false;
        }
    }
}
