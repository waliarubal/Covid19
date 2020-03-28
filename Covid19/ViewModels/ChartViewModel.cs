using Covid19.Models;
using Covid19.Services;
using Covid19.Shared.Base;
using Covid19.Shared.Commands;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Covid19.ViewModels
{
    public class ChartViewModel : ViewModelBase
    {
        ICommand _refresh;
        IEnumerable<Case> _cases;
        readonly ICovid19Service _covidService;

        public ChartViewModel(ICovid19Service covidService)
        {
            Title = "Statistics";

            _covidService = covidService;

            RefreshCommand.Execute(null);
        }

        public override bool IsCachable => true;

        public ObservableCollection<Case> Cases
        {
            get => Get<ObservableCollection<Case>>();
            private set => Set(value);
        }

        public ICommand RefreshCommand
        {
            get
            {
                if (_refresh == null)
                    _refresh = new RelayCommand(RefreshAction) { IsAsynchronous = true };

                return _refresh;
            }
        }

        async void RefreshAction()
        {
            IsBusy = true;

            _cases = await _covidService.GetCases();
            Cases = new ObservableCollection<Case>(_cases);

            IsBusy = false;
        }
    }
}
