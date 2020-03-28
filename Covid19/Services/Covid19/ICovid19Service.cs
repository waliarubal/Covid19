using Covid19.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Covid19.Services
{
    public interface ICovid19Service
    {
        Task<IEnumerable<QuestionAnswer>> GetQuestionAnswers();

        Task<IEnumerable<Case>> GetCases();
    }
}
