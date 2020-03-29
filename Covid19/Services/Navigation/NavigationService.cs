using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Covid19.Services
{
    public class NavigationService : INavigationService
    {
        internal delegate void RequestNavigation(View view);
        internal event RequestNavigation NavigationRequested;

        public void ShowMenu()
        {
            if (Application.Current.MainPage is MasterDetailPage mainView)
                mainView.IsPresented = true;
        }

        public void Quit()
        {
            Process.GetCurrentProcess().CloseMainWindow();
        }

        public async Task Navigate(Type viewType, bool isModal = false)
        {
            if (isModal)
            {
                var modalView = Activator.CreateInstance(viewType) as Page;
                if (modalView == null)
                    return;

                var mainView = Application.Current.MainPage as MasterDetailPage;
                if (mainView == null)
                    return;

                // close any open modal view
                if (mainView.Navigation.ModalStack.Count > 0)
                    await mainView.Navigation.PopModalAsync();

                // hide menu
                mainView.IsPresented = false;

                await mainView.Navigation.PushModalAsync(modalView);
                return;
            }

            var handler = NavigationRequested;
            if (handler == null)
                return;

            var view = Activator.CreateInstance(viewType) as View;
            if (view == null)
                return;

            handler.Invoke(view);
        }

        public async Task Close()
        {
            var mainView = Application.Current.MainPage as MasterDetailPage;
            if (mainView == null)
                return;

            if (mainView.Navigation.ModalStack.Count > 0)
                await mainView.Navigation.PopModalAsync();
        }
    }
}
