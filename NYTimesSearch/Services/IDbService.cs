using NYTimesSearch.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NYTimesSearch.Services
{
    /// <summary>
    /// Db service interface
    /// </summary>
    public interface IDbService
    {
        /// <summary>
        /// Saving serach item for particular user
        /// </summary>
        /// <param name="userSearch">Object containing username and items to save in db</param>
        /// <returns>True if success</returns>
        Task<bool> SaveNewUserSearch(UserSearch userSearch);

        /// <summary>
        /// Get all searched items per user
        /// </summary>
        /// <param name="userName">username</param>
        /// <returns>List of searched items</returns>
        Task<List<string>> GetAllSearchesPerUser(string userName);

        /// <summary>
        /// Cheking if user exist
        /// </summary>
        /// <param name="userName">User name</param>
        /// <returns>True if exist</returns>
        Task<bool> CheckIfUserExist(string userName);
    }
}