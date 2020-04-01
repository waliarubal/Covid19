using Covid19.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Covid19.Services
{
    public interface IJhuCsseService
    {
        Task<CaseCollection> GetCases(string keywoard);

        Task<IEnumerable<string>> GetRegions();
    }
}
