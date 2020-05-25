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
    public class UserSearchController : ApiController
    {
        private readonly INewsService _nytService;
        private readonly IDbService _dbService;

        public UserSearchController(INewsService newService, IDbService dbService)
        {
            _dbService = dbService;
            _nytService = newService;
        }

        [Route("api/getsearchresults")]
        public async Task<SearchResultsDto> GetSearchResults(SearchResultsDto search)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            SearchResults response = await _nytService.SearchNews(search.SearchItem, search.Page).ConfigureAwait(false);
            SearchResultsDto result = new SearchResultsDto();
            result.SearchResultsList = response.SearchResultsList;
            result.Page = search.Page;
            result.SearchItem = search.SearchItem;
            await _dbService.SaveNewUserSearch(new UserSearch() { UserName = search.UserName, SearchItem = search.SearchItem.Trim() });

            return result;
        }

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