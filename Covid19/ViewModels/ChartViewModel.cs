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
        ICommand _refresh, _refreshRegionalView;
        readonly IJhuCsseService _jhuCsseService;

        public ChartViewModel(IJhuCsseService jhuCsseService)
        {
            Title = "Graphical View";
            _jhuCsseService = jhuCsseService;

            RefreshCommand.Execute(null);
        }

        #region properties

        public CaseCollection TopFiveCases
        {
            get => Get<CaseCollection>();
            private set => Set(value);
        }

        public ObservableCollection<string> Regions
        {
            get => Get<ObservableCollection<string>>();
            private set => Set(value);
        }

        public string SelectedRegion
        {
            get => Get<string>();
            set => Set(value);
        }

        public CaseCollection SelectedRegionCase
        {
            get => Get<CaseCollection>();
            private set => Set(value);
        }

        public CaseCollection BottomFiveCases
        {
            get => Get<CaseCollection>();
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

        public ICommand RefreshRegionalViewCommand
        {
            get
            {
                if (_refreshRegionalView == null)
                    _refreshRegionalView = new RelayCommand<string>(RefreshRegionalViewAction);

                return _refreshRegionalView;
            }
        }

        #endregion

        async void RefreshRegionalViewAction(string region)
        {
            if (string.IsNullOrEmpty(region))
                return;

            IsBusy = true;

            var caseRecord = await _jhuCsseService.GetCases(region);
            SelectedRegionCase = caseRecord;

            IsBusy = false;
        }

        async void RefreshAction()
        {
            IsBusy = true;

            var cases = await _jhuCsseService.GetCases(null);

            var regions = await _jhuCsseService.GetRegions();
            Regions = new ObservableCollection<string>(regions);

            CaseCollection topFive, bottomFive;
            TopFive(cases, out topFive, out bottomFive);
            
            TopFiveCases = topFive;
            BottomFiveCases = bottomFive;

            IsBusy = false;
        }

        void TopFive(CaseCollection cases, out CaseCollection topFiveCases, out CaseCollection bottomFiveCases)
        {
            var topFive = new Case[5];
            for (var index = 0; index < 5; index++)
                topFive[index] = new Case();

            var bottomFive = new Case[5];
            for (var index = 0; index < 5; index++)
                bottomFive[index] = new Case() { Confirmed = long.MaxValue };

            if (cases.Cases.Count < 5)
            {
                topFiveCases = bottomFiveCases = default;
                return;
            }

            for(var index = 0; index < cases.Cases.Count; index++)
            {
                if (cases.Cases[index].Confirmed > topFive[0].Confirmed)
                {
                    topFive[4] = topFive[3];
                    topFive[3] = topFive[2];
                    topFive[2] = topFive[1];
                    topFive[1] = topFive[0];
                    topFive[0] = cases.Cases[index];
                }
                else if (cases.Cases[index].Confirmed > topFive[1].Confirmed)
                {
                    topFive[4] = topFive[3];
                    topFive[3] = topFive[2];
                    topFive[2] = topFive[1];
                    topFive[1] = cases.Cases[index];
                }
                else if (cases.Cases[index].Confirmed > topFive[2].Confirmed)
                {
                    topFive[4] = topFive[3];
                    topFive[3] = topFive[2];
                    topFive[2] = cases.Cases[index];
                }
                else if (cases.Cases[index].Confirmed > topFive[3].Confirmed)
                {
                    topFive[4] = topFive[3];
                    topFive[3] = cases.Cases[index];
                }
                else if (cases.Cases[index].Confirmed > topFive[4].Confirmed)
                    topFive[4] = cases.Cases[index];

                if (cases.Cases[index].Confirmed < bottomFive[0].Confirmed)
                {
                    bottomFive[4] = bottomFive[3];
                    bottomFive[3] = bottomFive[2];
                    bottomFive[2] = bottomFive[1];
                    bottomFive[1] = bottomFive[0];
                    bottomFive[0] = cases.Cases[index];
                }
                else if (cases.Cases[index].Confirmed < bottomFive[1].Confirmed)
                {
                    bottomFive[4] = bottomFive[3];
                    bottomFive[3] = bottomFive[2];
                    bottomFive[2] = bottomFive[1];
                    bottomFive[1] = cases.Cases[index];
                }
                else if (cases.Cases[index].Confirmed < bottomFive[2].Confirmed)
                {
                    bottomFive[4] = bottomFive[3];
                    bottomFive[3] = bottomFive[2];
                    bottomFive[2] = cases.Cases[index];
                }
                else if (cases.Cases[index].Confirmed < bottomFive[3].Confirmed)
                {
                    bottomFive[4] = bottomFive[3];
                    bottomFive[3] = cases.Cases[index];
                }
                else if (cases.Cases[index].Confirmed < bottomFive[4].Confirmed)
                    bottomFive[4] = cases.Cases[index];
            }

            topFiveCases = new CaseCollection(topFive);
            bottomFiveCases = new CaseCollection(bottomFive);
        }
    }
}
