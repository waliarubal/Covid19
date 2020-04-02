using Covid19.Services;
using Covid19.Shared.Base;
using Covid19.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace Covid19.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        readonly INavigationService _navigationService;
        readonly ISettingsService _settingsService;
        readonly IJhuCsseService _jhuCcseService;
        ICommand _save, _refreshRegions;

        public SettingsViewModel(
            INavigationService navigationService, 
            ISettingsService settingsService, 
            IJhuCsseService jhuCcseService)
        {
            _navigationService = navigationService;
            _settingsService = settingsService;
            _jhuCcseService = jhuCcseService;

            RefreshRegionsCommand.Execute(null);
        }

        #region properties

        public bool IsGraphical
        {
            get => _settingsService.Get<bool>(nameof(IsGraphical));
            set
            {
                _settingsService.Set(nameof(IsGraphical), value);
                RaisePropertyChanged();
            }
        }

        public bool IsTotal
        {
            get => _settingsService.Get<bool>(nameof(IsTotal));
            set
            {
                _settingsService.Set(nameof(IsTotal), value);
                RaisePropertyChanged();
            }
        }

        public bool IsAlJazeera
        {
            get => _settingsService.Get<bool>(nameof(IsAlJazeera));
            set
            {
                _settingsService.Set(nameof(IsAlJazeera), value);
                RaisePropertyChanged();
            }
        }

        public bool IsBbc
        {
            get => _settingsService.Get<bool>(nameof(IsBbc));
            set
            {
                _settingsService.Set(nameof(IsBbc), value);
                RaisePropertyChanged();
            }
        }

        public bool IsCnn
        {
            get => _settingsService.Get<bool>(nameof(IsCnn));
            set
            {
                _settingsService.Set(nameof(IsCnn), value);
                RaisePropertyChanged();
            }
        }

        public bool IsTheNewYorkTimes
        {
            get => _settingsService.Get<bool>(nameof(IsTheNewYorkTimes));
            set
            {
                _settingsService.Set(nameof(IsTheNewYorkTimes), value);
                RaisePropertyChanged();
            }
        }

        public bool IsTimesOfIndia
        {
            get => _settingsService.Get<bool>(nameof(IsTimesOfIndia));
            set
            {
                _settingsService.Set(nameof(IsTimesOfIndia), value);
                RaisePropertyChanged();
            }
        }

        public bool IsYahooNews
        {
            get => _settingsService.Get<bool>(nameof(IsYahooNews));
            set
            {
                _settingsService.Set(nameof(IsYahooNews), value);
                RaisePropertyChanged();
            }
        }

        public string DefaultRegion
        {
            get => _settingsService.Get<string>(nameof(DefaultRegion));
            set
            {
                _settingsService.Set(nameof(DefaultRegion), value);
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<string> Regions
        {
            get => Get<ObservableCollection<string>>();
            private set => Set(value);
        }

        public ICommand SaveCommand
        {
            get
            {
                if (_save == null)
                    _save = new RelayCommand(SaveAction);

                return _save;
            }
        }

        public ICommand RefreshRegionsCommand
        {
            get
            {
                if (_refreshRegions == null)
                    _refreshRegions = new RelayCommand(RefreshRegionsAction);

                return _refreshRegions;
            }
        }

        public override bool IsCachable => false;

        #endregion

        async void RefreshRegionsAction()
        {
            IsBusy = true;

            var regions = await _jhuCcseService.GetRegions();
            Regions = new ObservableCollection<string>(regions);

            IsBusy = false;
        }

        async void SaveAction()
        {
            await _settingsService.Save();
            await _navigationService.Close();
        }
    }
}
