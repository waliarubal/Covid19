using Covid19.Shared.Base;

namespace Covid19.ViewModels
{
    public class ChartViewModel : ViewModelBase
    {

        public ChartViewModel()
        {
            Title = "Statistics";
        }

        public override bool IsCachable => true;
    }
}
