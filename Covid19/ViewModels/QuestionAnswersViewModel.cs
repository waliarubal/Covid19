using Covid19.Models;
using Covid19.Services;
using Covid19.Shared.Base;
using Covid19.Shared.Commands;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Covid19.ViewModels
{
    public class QuestionAnswersViewModel : ViewModelBase
    {
        ICommand _refresh;
        readonly IWhoService _whoService;

        public QuestionAnswersViewModel(IWhoService whoService)
        {
            Title = "Question & Answers";

            _whoService = whoService;

            RefreshCommand.Execute(null);
        }

        public override bool IsCachable => true;

        public ObservableCollection<QuestionAnswer> QuestionAnswers
        {
            get => Get<ObservableCollection<QuestionAnswer>>();
            private set => Set(value);
        }

        public ICommand RefreshCommand
        {
            get
            {
                if (_refresh == null)
                    _refresh = new RelayCommand(RefreshAction) { IsAsynchronous = true };

                return _refresh;
            }
        }

        async void RefreshAction()
        {
            IsBusy = true;

            var questionAnswers = await _whoService.GetQuestionAnswers();
            QuestionAnswers = new ObservableCollection<QuestionAnswer>(questionAnswers);

            IsBusy = false;
        }
    }
}
