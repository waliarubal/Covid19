using Covid19.Shared.Base;

namespace Covid19.ViewModels
{
    public class NewsViewModel : ViewModelBase
    {
        public NewsViewModel()
        {
            Title = "News Updates";
        }

        public override bool IsCachable => true;
    }
}
