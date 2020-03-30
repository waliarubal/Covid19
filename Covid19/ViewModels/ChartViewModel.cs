using Covid19.Shared.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Covid19.ViewModels
{
    public class ChartViewModel : ViewModelBase
    {
        public ChartViewModel()
        {
            Title = "Graphical View";
        }

        public override bool IsCachable => true;
    }
}
