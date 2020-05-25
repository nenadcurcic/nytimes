using NYTimesSearch.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NYTimesSearch.Services
{
    public interface IDbService
    {
        Task<bool> SaveNewUserSearch(UserSearch userSearch);
        Task<List<string>> GetAllSearchesPerUser(string userName);
        Task<bool> CheckIfUserExist(string userName);
    }
}