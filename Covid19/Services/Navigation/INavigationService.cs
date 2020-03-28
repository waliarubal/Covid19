using System;

namespace Covid19.Services
{
    public interface INavigationService
    {

        void ShowMenu();

        void Quit();

        void Navigate(Type viewType, bool isModal = false);
    }
}
