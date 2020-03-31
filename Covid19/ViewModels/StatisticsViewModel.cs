using Covid19.Services;
using Covid19.Shared.Base;
using Covid19.Views;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Covid19.ViewModels
{
    public class StatisticsViewModel : ViewModelBase
    {
        readonly INavigationService _navigationService;

        public StatisticsViewModel(INavigationService navigationService, ISettingsService settingsService)
        {
            _navigationService = navigationService;
            Title = "Statistics";
            IsGraphical = settingsService.Get<bool>(nameof(SettingsViewModel.IsGraphical));
        }

        public override bool IsCachable => true;

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

        public bool IsGraphical
        {
            get => Get<bool>();
            set
            {
                Set(value);
                var viewType = value ? typeof(ChartView) : typeof(TabularView);
                Navigate(viewType);
            }
        }

        void Navigate(Type viewType)
        {
            ChildView = _navigationService.GetView(viewType);

            var viewModel = ChildView.BindingContext as ViewModelBase;
            if (viewModel != null)
                ChildViewTitle = viewModel.Title;
            else
                ChildViewTitle = string.Empty;
        }
    }
}
