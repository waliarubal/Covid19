using Covid19.Services;
using Covid19.Shared.Base;
using Covid19.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Covid19.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        readonly INavigationService _navigationService;
        readonly ISettingsService _settingsService;
        ICommand _save;

        public SettingsViewModel(INavigationService navigationService, ISettingsService settingsService)
        {
            _navigationService = navigationService;
            _settingsService = settingsService;
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
                RaisePropertyChanged(nameof(IsTheNewYorkTimes));
            }
        }

        public bool IsTimesOfIndia
        {
            get => _settingsService.Get<bool>(nameof(IsTimesOfIndia));
            set
            {
                _settingsService.Set(nameof(IsTimesOfIndia), value);
                RaisePropertyChanged(nameof(IsTimesOfIndia));
            }
        }

        public bool IsYahooNews
        {
            get => _settingsService.Get<bool>(nameof(IsYahooNews));
            set
            {
                _settingsService.Set(nameof(IsYahooNews), value);
                RaisePropertyChanged(nameof(IsYahooNews));
            }
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

        public override bool IsCachable => false;

        #endregion

        async void SaveAction()
        {
            await _settingsService.Save();
            await _navigationService.Close();
        }
    }
}
