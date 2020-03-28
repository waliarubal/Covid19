using Covid19.Services;
using Covid19.Shared;
using Covid19.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace Covid19
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            RegisterDependencies();

            MainPage = new MainView();
        }

        void RegisterDependencies()
        {
            ServiceLocator.Instance.RegisterSingleton<INavigationService, NavigationService>();
            ServiceLocator.Instance.RegisterSingleton<ICovid19Service, Covid19Service>();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
