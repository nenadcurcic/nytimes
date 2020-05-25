using Microsoft.Ajax.Utilities;
using NYTimesSearch.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace NYTimesSearch.Services
{
    //TODO: comments
    public class DbService : IDisposable, IDbService
    {
        private readonly ApplicationDbContext _context;

        public DbService()
        {
            _context = new ApplicationDbContext();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

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

        public async Task<bool> CheckIfUserExist(string userName)
        {
            return await _context.Users.AnyAsync(u => u.UserName == userName);
        }

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