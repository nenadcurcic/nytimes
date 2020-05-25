using NYTimesSearch.Models;
using NYTimesSearch.Services;
using NYTimesSearch.ViewModels;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace NYTimesSearch.Controllers
{
/// <summary>
/// News Controller
/// </summary>
    public class UserSearchController : Controller
    {
        private readonly INewsService _nytService;
        private readonly IDbService _dbService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="nytService">News Service DI</param>
        /// <param name="dbService">Db Service DI</param>
        public UserSearchController(INewsService nytService, IDbService dbService)
        {
            _nytService = nytService;
            _dbService = dbService;
        }

        // GET: UserSearch
        [Authorize]
        public ActionResult Index()
        {
            return View("UserSearchForm", new SearchResultsViewModel());
        }

        /// <summary>
        /// Displaying search results
        /// </summary>
        /// <param name="itemToSearch"></param>
        /// <returns></returns>
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SearchNews(SearchResultsViewModel itemToSearch)
        {
            SearchResultsViewModel res = await _nytService.SearchNews(itemToSearch.SearchItem, itemToSearch.Page);
            await _dbService.SaveNewUserSearch(new UserSearch() { UserName = this.User.Identity.Name, SearchItem = itemToSearch.SearchItem.Trim() });
            return View("SearchResults", res);
        }
    }
}