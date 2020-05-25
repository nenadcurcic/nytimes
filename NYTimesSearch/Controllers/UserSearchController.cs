using NYTimesSearch.Models;
using NYTimesSearch.Services;
using NYTimesSearch.ViewModels;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace NYTimesSearch.Controllers
{
    public class UserSearchController : Controller
    {
        private readonly INewsService _nytService;
        private readonly IDbService _dbService;

        public UserSearchController(INewsService nytService, IDbService dbService)
        {
            _nytService = nytService;
            _dbService = dbService;
        }

        // GET: UserSearch
        [Authorize]
        public ActionResult Index()
        {
            UserSearch thisUser = new UserSearch() { UserName = this.User.Identity.Name };
            return View("UserSearchForm", new SearchResults());
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SearchNews(SearchResults itemToSearch)
        {
            SearchResults res = await _nytService.SearchNews(itemToSearch.SearchItem, itemToSearch.Page);
            await _dbService.SaveNewUserSearch(new UserSearch() { UserName = this.User.Identity.Name, SearchItem = itemToSearch.SearchItem.Trim() });
            return View("SearchResults", res);
        }
    }
}