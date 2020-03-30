using Covid19.Services;
using Covid19.Shared.Base;
using Covid19.Shared.Commands;
using Covid19.Views;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Covid19.ViewModels
{
    public class ContainerViewModel : ViewModelBase
    {
        ICommand _navigate;
        readonly NavigationService _navigationService;

        public ContainerViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService as NavigationService;
            _navigationService.NavigationRequested += OnNavigationRequested;

            NavigateCommand.Execute(typeof(StatisticsView));
        }
        ~ContainerViewModel()
        {
            _navigationService.NavigationRequested -= OnNavigationRequested;
        }

        #region properties

        public override bool IsCachable => false;

        public View ChildView
        {
            get => Get<View>();
            private set => Set(value);
        }

        public string ChildViewTitle
        {
            get => Get<string>();
            private set => Set(value);
        }

        public ICommand NavigateCommand
        {
            get
            {
                if (_navigate == null)
                    _navigate = new RelayCommand<Type>(NavigateAction);

                return _navigate;
            }
        }

        #endregion

        void OnNavigationRequested(View view)
        {
            ChildView = view;

            var viewModel = view.BindingContext as ViewModelBase;
            if (viewModel != null)
                ChildViewTitle = viewModel.Title;
            else
                ChildViewTitle = string.Empty;
        }

        async void NavigateAction(Type viewType)
        {
            await _navigationService.Navigate(viewType);
        }
    }
}
