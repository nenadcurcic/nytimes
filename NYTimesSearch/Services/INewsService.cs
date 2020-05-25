using NYTimesSearch.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NYTimesSearch.Services
{
    public interface INewsService
    {
        Task<SearchResultsViewModel> SearchNews(string keywords, string page);
    }
}
