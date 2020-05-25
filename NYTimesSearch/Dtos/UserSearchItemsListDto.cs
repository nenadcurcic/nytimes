using System.Collections.Generic;

namespace NYTimesSearch.Dtos
{
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