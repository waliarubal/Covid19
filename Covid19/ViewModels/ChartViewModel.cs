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
        ICommand _refresh;
        readonly IJhuCsseService _jhuCsseService;

        public ChartViewModel(IJhuCsseService jhuCsseService)
        {
            Title = "Graphical View";
            _jhuCsseService = jhuCsseService;

            RefreshCommand.Execute(null);
        }

        #region properties

        public ObservableCollection<Case> TopFiveCases
        {
            get => Get<ObservableCollection<Case>>();
            private set => Set(value);
        }

        public ObservableCollection<Case> BottomFiveCases
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
            var cases = await _jhuCsseService.GetCases(null);

            IEnumerable<Case> topFive, bottomFive;
            TopFive(new List<Case>(cases), out topFive, out bottomFive);

            TopFiveCases = new ObservableCollection<Case>(topFive);
            BottomFiveCases = new ObservableCollection<Case>(bottomFive);
        }

        void TopFive(IList<Case> cases, out IEnumerable<Case> topFiveCases, out IEnumerable<Case> bottomFiveCases)
        {
            var topFive = new Case[5];
            for (var index = 0; index < 5; index++)
                topFive[index] = new Case();

            var bottomFive = new Case[5];
            for (var index = 0; index < 5; index++)
                bottomFive[index] = new Case() { Confirmed = long.MaxValue };

            if (cases.Count < 5)
            {
                topFiveCases = bottomFiveCases = default;
                return;
            }

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

                if (cases[index].Confirmed < bottomFive[0].Confirmed)
                {
                    bottomFive[4] = bottomFive[3];
                    bottomFive[3] = bottomFive[2];
                    bottomFive[2] = bottomFive[1];
                    bottomFive[1] = bottomFive[0];
                    bottomFive[0] = cases[index];
                }
                else if (cases[index].Confirmed < bottomFive[1].Confirmed)
                {
                    bottomFive[4] = bottomFive[3];
                    bottomFive[3] = bottomFive[2];
                    bottomFive[2] = bottomFive[1];
                    bottomFive[1] = cases[index];
                }
                else if (cases[index].Confirmed < bottomFive[2].Confirmed)
                {
                    bottomFive[4] = bottomFive[3];
                    bottomFive[3] = bottomFive[2];
                    bottomFive[2] = cases[index];
                }
                else if (cases[index].Confirmed < bottomFive[3].Confirmed)
                {
                    bottomFive[4] = bottomFive[3];
                    bottomFive[3] = cases[index];
                }
                else if (cases[index].Confirmed < bottomFive[4].Confirmed)
                    bottomFive[4] = cases[index];
            }

            topFiveCases = new List<Case>(topFive);
            bottomFiveCases = new List<Case>(bottomFive);
        }
    }
}
