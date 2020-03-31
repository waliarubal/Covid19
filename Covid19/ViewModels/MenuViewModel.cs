using Covid19.Services;
using Covid19.Shared.Base;
using Covid19.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;

namespace Covid19.ViewModels
{
    public class MenuViewModel : ViewModelBase
    {
        ICommand _navigate;
        readonly INavigationService _navigationService;

        public MenuViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        #region properties

        public ICommand NavigateCommand
        {
            get
            {
                if (_navigate == null)
                    _navigate = new RelayCommand<Type>(NavigateAction);

                return _navigate;
            }
        }

        public string VersionString => $"Version {VersionTracking.CurrentVersion} (Build {VersionTracking.CurrentBuild})";

        public override bool IsCachable => false;

        #endregion

        async void NavigateAction(Type viewType)
        {
            await _navigationService.Navigate(viewType, true);
        }
    }
}
