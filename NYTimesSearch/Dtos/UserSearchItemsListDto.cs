using System.Collections.Generic;

namespace NYTimesSearch.Dtos
{
    /// <summary>
    /// Searched items per user DTO used for web API
    /// </summary>
    public class UserSearchItemsListDto
    {
        public string UserName { get; set; }
        public List<string> SearchedItems { get; set; }

        public UserSearchItemsListDto()
        {
            SearchedItems = new List<string>();
        }
    }
}