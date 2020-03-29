﻿using Covid19.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Covid19.Services
{
    public class JhuCsseService: IJhuCsseService
    {
        const string COUNTRY_WISE_CASE_URL = "https://raw.githubusercontent.com/CSSEGISandData/COVID-19/web-data/data/cases_country.csv";

        public async Task<IEnumerable<Case>> GetCases()
        {
            var cases = new List<Case>();

            var culture = CultureInfo.InvariantCulture;

            using (var client = new WebClient())
            {
                var csvData = await client.DownloadStringTaskAsync(COUNTRY_WISE_CASE_URL);
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
    }
}