using NYTimesSearch.ViewModels;
using System.Threading.Tasks;

namespace NYTimesSearch.Services
{
    /// <summary>
    /// News Service Interface
    /// </summary>
    public interface INewsService
    {
        /// <summary>
        /// Search news on News service
        /// </summary>
        /// <param name="keywords">keywords to search</param>
        /// <param name="page">Used for pagination</param>
        /// <returns>Search results</returns>
        Task<SearchResultsViewModel> SearchNews(string keywords, string page);
    }
}