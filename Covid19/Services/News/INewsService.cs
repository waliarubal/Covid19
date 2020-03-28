using Covid19.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Covid19.Services
{
    public interface INewsService
    {
        Task<IEnumerable<News>> GetFeed(NewsSource source);
    }
}
