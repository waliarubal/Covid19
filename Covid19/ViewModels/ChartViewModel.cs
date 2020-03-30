using Covid19.Models;
using Covid19.Services;
using Covid19.Shared.Base;
using Covid19.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace Covid19.ViewModels
{
    public class ChartViewModel : ViewModelBase
    {
        ICommand _drawChart, _refresh;
        readonly IJhuCsseService _jhuCsseService;

        public ChartViewModel(IJhuCsseService jhuCsseService)
        {
            Title = "Graphical View";
            _jhuCsseService = jhuCsseService;

            RefreshCommand.Execute(null);
        }

        #region properties

        public ObservableCollection<Case> Cases
        {
            get => Get<ObservableCollection<Case>>();
            private set => Set(value);
        }

        public override bool IsCachable => true;

        public ICommand RefreshCommand
        {
            get
            {
                if (_refresh == null)
                    _refresh = new RelayCommand(RefreshAction) { IsAsynchronous = true };

                return _refresh;
            }
        }

        #endregion

        async void RefreshAction()
        {
            var cases = await _jhuCsseService.GetCases();
            cases = TopFive(new List<Case>(cases));
            Cases = new ObservableCollection<Case>(cases);
        }

        IEnumerable<Case> TopFive(IList<Case> cases)
        {
            var topFive = new Case[5];
            for (var index = 0; index < 5; index++)
                topFive[index] = new Case();

            if (cases.Count < 5)
                return cases;

            for(var index = 0; index < cases.Count; index++)
            {
                if (cases[index].Confirmed > topFive[0].Confirmed)
                {
                    topFive[4] = topFive[3];
                    topFive[3] = topFive[2];
                    topFive[2] = topFive[1];
                    topFive[1] = topFive[0];
                    topFive[0] = cases[index];
                }
                else if (cases[index].Confirmed > topFive[1].Confirmed)
                {
                    topFive[4] = topFive[3];
                    topFive[3] = topFive[2];
                    topFive[2] = topFive[1];
                    topFive[1] = cases[index];
                }
                else if (cases[index].Confirmed > topFive[2].Confirmed)
                {
                    topFive[4] = topFive[3];
                    topFive[3] = topFive[2];
                    topFive[2] = cases[index];
                }
                else if (cases[index].Confirmed > topFive[3].Confirmed)
                {
                    topFive[4] = topFive[3];
                    topFive[3] = cases[index];
                }
                else if (cases[index].Confirmed > topFive[4].Confirmed)
                    topFive[4] = cases[index];
            }

            return new List<Case>(topFive);
        }
    }
}
