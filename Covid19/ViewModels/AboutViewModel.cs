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
    public class AboutViewModel : ViewModelBase
    {
        ICommand _close;
        readonly INavigationService _navigationService;

        public AboutViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        #region properties

        public override bool IsCachable => false;

        public string VersionString => $"Version {VersionTracking.CurrentVersion} (Build {VersionTracking.CurrentBuild})";

        public ICommand CloseCommand
        {
            get
            {
                if (_close == null)
                    _close = new RelayCommand(CloseAction);

                return _close;
            }
        }

        #endregion

        async void CloseAction()
        {
            await _navigationService.Close();
        }
    }
}
