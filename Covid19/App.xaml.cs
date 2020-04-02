using Covid19.Services;
using Covid19.Shared;
using Covid19.ViewModels;
using Covid19.Views;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace Covid19
{
    public partial class App : Application
    {
        const string SYNCFUSION_LICENE_KEY = "MjMyMjg0QDMxMzgyZTMxMmUzMEkvZFl5Z3dMbUtDbTVvc2puaEZ4NG02TGZwMGgvZ1dXd3pHQkJuWDlwUU09";

        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(SYNCFUSION_LICENE_KEY);

            InitializeComponent();
            VersionTracking.Track();
            RegisterDependencies();

            MainPage = new MainView();
        }

        void RegisterDependencies()
        {
            ServiceLocator.Instance.RegisterSingleton<INavigationService, NavigationService>();
            ServiceLocator.Instance.RegisterSingleton<ISettingsService, SettingsService>();
            ServiceLocator.Instance.RegisterSingleton<IWhoService, WhoService>();
            ServiceLocator.Instance.RegisterSingleton<IJhuCsseService, JhuCsseService>();
            ServiceLocator.Instance.RegisterSingleton<INewsService, NewsService>();
        }

        async Task ApplyDefaultSettings()
        {
            if (!VersionTracking.IsFirstLaunchEver)
                return;

            var settingsService = ServiceLocator.Instance.Resolve<ISettingsService>();
            settingsService.Set(nameof(SettingsViewModel.IsBbc), true);
            settingsService.Set(nameof(SettingsViewModel.IsGraphical), true);
            settingsService.Set(nameof(SettingsViewModel.IsTotal), true);
            settingsService.Set(nameof(SettingsViewModel.DefaultRegion), "India");
            await settingsService.Save();
        }

        protected override void OnStart()
        {
            Task.Run(async () => await ApplyDefaultSettings());
        }

        protected override void OnSleep()
        {

        }

        protected override void OnResume()
        {
        }
    }
}
