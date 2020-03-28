using Covid19.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Covid19.Services
{
    public interface IWhoService
    {
        Task<IEnumerable<QuestionAnswer>> GetQuestionAnswers();
    }
}
