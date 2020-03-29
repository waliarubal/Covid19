using System;
using System.Threading.Tasks;

namespace Covid19.Services
{
    public interface INavigationService
    {

        void ShowMenu();

        void Quit();

        Task Navigate(Type viewType, bool isModal = false);
    }
}
