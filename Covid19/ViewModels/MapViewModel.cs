using Covid19.Shared.Base;

namespace Covid19.ViewModels
{
    public class MapViewModel : ViewModelBase
    {
        public MapViewModel()
        {
            Title = "World Map";
        }

        public override bool IsCachable => true;
    }
}
