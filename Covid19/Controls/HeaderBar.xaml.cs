using Covid19.Services;
using Covid19.Shared;
using Covid19.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Covid19.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HeaderBar : ContentView
    {
        public static readonly BindableProperty TitleProperty, IsMenuAllowedProperty;
        INavigationService _navigationService;
        ICommand _showMenu;

        static HeaderBar()
        {
            TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(HeaderBar), string.Empty, BindingMode.OneWay);
            IsMenuAllowedProperty = BindableProperty.Create(nameof(IsMenuAllowed), typeof(bool), typeof(HeaderBar), true, BindingMode.OneWay);
        }

        public HeaderBar()
        {
            InitializeComponent();
        }

        #region properties

        INavigationService NavigationService
        {
            get
            {
                if (_navigationService == null)
                    _navigationService = ServiceLocator.Instance.Resolve<INavigationService>();

                return _navigationService;
            }
        }

        public ICommand ShowMenuCommand
        {
            get
            {
                if (_showMenu == null)
                    _showMenu = new RelayCommand(ShowMenuAction);

                return _showMenu;
            }
        }

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public bool IsMenuAllowed
        {
            get => (bool)GetValue(IsMenuAllowedProperty);
            set => SetValue(IsMenuAllowedProperty, value);
        }

        #endregion

        void ShowMenuAction()
        {
            NavigationService.ShowMenu();
        }
    }
}