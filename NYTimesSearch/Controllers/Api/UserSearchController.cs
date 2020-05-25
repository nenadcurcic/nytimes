using Microsoft.Ajax.Utilities;
using NYTimesSearch.Dtos;
using NYTimesSearch.Models;
using NYTimesSearch.Services;
using NYTimesSearch.ViewModels;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace NYTimesSearch.Controllers.Api
{
    /// <summary>
    /// API Controller
    /// </summary>
    public class UserSearchController : ApiController
    {
        private readonly INewsService _nytService;
        private readonly IDbService _dbService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="newsService">News Service DI</param>
        /// <param name="dbService">Db Service DI</param>
        public UserSearchController(INewsService newsService, IDbService dbService)
        {
            _dbService = dbService;
            _nytService = newsService;
        }

        /// <summary>
        /// Search News
        /// </summary>
        /// <param name="search">Keywords and username</param>
        /// <returns>Search results</returns>
        [Route("api/getsearchresults")]
        public async Task<SearchResultsDto> GetSearchResults(SearchResultsDto search)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            SearchResultsViewModel response = await _nytService.SearchNews(search.SearchItem, search.Page).ConfigureAwait(false);
            SearchResultsDto result = new SearchResultsDto();
            result.SearchResultsList = response.SearchResultsList;
            result.Page = search.Page;
            result.SearchItem = search.SearchItem;
            await _dbService.SaveNewUserSearch(new UserSearch() { UserName = search.UserName, SearchItem = search.SearchItem.Trim() });

            return result;
        }

        /// <summary>
        /// Get all searched items for user
        /// </summary>
        /// <param name="userName">User name</param>
        /// <returns>List of searched items</returns>
        [Route("api/GetSearchedItemsPerUser")]
        public async Task<UserSearchItemsListDto> GetSearchedItemsPerUser(string userName)
        {
            UserSearchItemsListDto result = new UserSearchItemsListDto();
            result.UserName = userName;
            if (!userName.IsNullOrWhiteSpace())
            {
                result.SearchedItems = await _dbService.GetAllSearchesPerUser(userName);
            }

            return result;
        }
    }
}