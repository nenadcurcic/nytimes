using NYTimesSearch.ViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NYTimesSearch.Dtos
{
    /// <summary>
    /// Search results DTo used for web API controller
    /// </summary>
    public class SearchResultsDto
    {
        public List<SearchResultItemViewModel> SearchResultsList { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Page { get; set; }

        [Required]
        public string SearchItem { get; set; }

        public SearchResultsDto()
        {
            SearchResultsList = new List<SearchResultItemViewModel>();
            Page = "1";
        }
    }
}