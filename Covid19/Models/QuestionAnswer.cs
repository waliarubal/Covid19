using Covid19.Shared.Base;
using System.Collections.Generic;

namespace Covid19.Models
{
    public class QuestionAnswer: ModelBase
    {
        public string Question
        {
            get => Get<string>();
            set => Set(value);
        }

        public IEnumerable<string> Answer
        {
            get => Get<IEnumerable<string>>();
            set => Set(value);
        }

        public override string ToString()
        {
            return Question;
        }
    }
}
