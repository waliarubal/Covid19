using System;
using System.Diagnostics;
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

        public void Navigate(Type viewType, bool isModal = false)
        {
            var handler = NavigationRequested;
            if (handler == null)
                return;

            var view = Activator.CreateInstance(viewType) as View;
            if (view == null)
                return;

            handler.Invoke(view);
        }
    }
}
