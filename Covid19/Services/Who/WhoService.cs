using AngleSharp;
using Covid19.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;

namespace Covid19.Services
{
    public class WhoService : IWhoService
    {
        const string WHO_QA_URL = "https://www.who.int/news-room/q-a-detail/q-a-coronaviruses";
        
        public async Task<IEnumerable<QuestionAnswer>> GetQuestionAnswers()
        {
            var config = Configuration.Default
                .WithDefaultLoader()
                .WithDefaultCookies();

            var browsingContext = BrowsingContext.New(config);

            using (var document = await browsingContext.OpenAsync(WHO_QA_URL))
            {
                var questionAnswers = new List<QuestionAnswer>();

                var questionNodes = document.QuerySelectorAll("div.qa-details__content div.sf-accordion > div.sf-accordion__panel");
                foreach (var questionNode in questionNodes)
                {
                    var question = questionNode.QuerySelector("a.sf-accordion__link").TextContent;
                    if (string.IsNullOrWhiteSpace(question))
                        continue;

                    var paragraphs = new List<string>();

                    var answerNodes = questionNode.QuerySelectorAll("div.sf-accordion__content > p");
                    foreach (var answerNode in answerNodes)
                    {
                        var answer = answerNode.TextContent;
                        if (string.IsNullOrWhiteSpace(answer))
                            continue;

                        paragraphs.Add(answer.Trim());
                    }

                    if (paragraphs.Count == 0)
                        continue;

                    var questionAnswer = new QuestionAnswer();
                    questionAnswer.Question = question.Trim();
                    questionAnswer.Answer = paragraphs;
                    questionAnswers.Add(questionAnswer);
                }

                return questionAnswers;
            }
        }
    }
}
