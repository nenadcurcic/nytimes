using Microsoft.Ajax.Utilities;
using NYTimesSearch.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace NYTimesSearch.Services
{
    /// <summary>
    /// Database service
    /// </summary>
    public class DbService : IDisposable, IDbService
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        public DbService()
        {
            _context = new ApplicationDbContext();
        }

        /// <summary>
        /// Disposing object
        /// </summary>
        public void Dispose()
        {
            _context.Dispose();
        }

        /// <summary>
        /// Saving serach item for particular user
        /// </summary>
        /// <param name="rawUserSearch">Object containing username and items to save in db</param>
        /// <returns>True if success</returns>
        public async Task<bool> SaveNewUserSearch(UserSearch rawUserSearch)
        {
            bool saved = false;
            UserSearch userSearch = NormalizeSearchItem(rawUserSearch);

            await Task.Run(async () =>
             {
                 if (!userSearch.UserName.IsNullOrWhiteSpace()
                     && !userSearch.SearchItem.IsNullOrWhiteSpace()
                   && await _context.UserSearches.AnyAsync(u => u.SearchItem.Contains(userSearch.SearchItem) && u.UserName == userSearch.UserName))
                 {
                     _context.UserSearches.Add(userSearch);
                     await _context.SaveChangesAsync();
                     saved = true;
                 }
             });
            return saved;
        }

        /// <summary>
        /// Get all searched items per user
        /// </summary>
        /// <param name="userName">username</param>
        /// <returns>List of searched items</returns>
        public async Task<List<string>> GetAllSearchesPerUser(string userName)
        {
            List<string> result = new List<string>();
            await Task.Run(() =>
            {
                if (!userName.IsNullOrWhiteSpace())
                {
                    result = _context.UserSearches.Where(u => u.UserName == userName).Select(u => u.SearchItem).ToList();
                }
            });
            return result;
        }

        /// <summary>
        /// Cheking if user exist
        /// </summary>
        /// <param name="userName">User name</param>
        /// <returns>True if exist</returns>
        public async Task<bool> CheckIfUserExist(string userName)
        {
            return await _context.Users.AnyAsync(u => u.UserName == userName);
        }

        /// <summary>
        /// Normalising search item - to lowercase and trimming
        /// </summary>
        /// <param name="search">Search obj to normalize</param>
        /// <returns>Normalized</returns>
        private UserSearch NormalizeSearchItem(UserSearch search)
        {
            return new UserSearch()
            {
                Id = search.Id,
                SearchItem = search.SearchItem.Trim().ToLower(),
                UserName = search.UserName
            };
        }
    }
}