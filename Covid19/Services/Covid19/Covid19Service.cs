using AngleSharp;
using Covid19.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;

namespace Covid19.Services
{
    public class Covid19Service : ICovid19Service
    {
        public async Task<IEnumerable<Case>> GetCases()
        {
            var cases = new List<Case>();

            var culture = CultureInfo.InvariantCulture;

            using (var client = new WebClient())
            {
                var csvData = await client.DownloadStringTaskAsync("https://raw.githubusercontent.com/CSSEGISandData/COVID-19/web-data/data/cases_country.csv");
                if (string.IsNullOrEmpty(csvData))
                    return cases;

                var lines = csvData
                    .Replace("\"Korea, South\"", "Korea South") // fix south korea name in CSV
                    .Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

                if (lines.Length == 0)
                    return cases;

                for (var index = 1; index < lines.Length; index++)
                {
                    var cells = lines[index].Split(',');

                    if (cells.Length < 8)
                        continue;

                    var cellIndex = 0;
                    var caseData = new Case();

                    caseData.Country = cells[cellIndex++];
                    caseData.LastUpdate = DateTime.ParseExact(cells[cellIndex++], "yyyy-MM-dd HH:mm:ss", culture);
                    caseData.Latitude = cells[cellIndex++];
                    caseData.Longitude = cells[cellIndex++];
                    caseData.Confirmed = long.Parse(cells[cellIndex++]);
                    caseData.Deaths = long.Parse(cells[cellIndex++]);
                    caseData.Recovered = long.Parse(cells[cellIndex++]);
                    caseData.Active = long.Parse(cells[cellIndex++]);
                    cases.Add(caseData);
                }
            }

            return cases;
        }

        public async Task<IEnumerable<QuestionAnswer>> GetQuestionAnswers()
        {
            var config = Configuration.Default
                .WithDefaultLoader()
                .WithDefaultCookies();

            var browsingContext = BrowsingContext.New(config);

            using (var document = await browsingContext.OpenAsync("https://www.who.int/news-room/q-a-detail/q-a-coronaviruses"))
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
