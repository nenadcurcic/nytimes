using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NYTimesSearch.ViewModels
{
    public class SearchResultItemViewModel
    {
        public string ArticleName { get; set; }
        public string ArticleLink { get; set; }
        public string ArticleSource { get; set; }
    }

    public class SearchResultsViewModel
    {
        public List<SearchResultItemViewModel> SearchResultsList { get; set; }
        public string Page { get; set; }

        [Required]
        [Display(Name = "Search item")]
        public string SearchItem { get; set; }

        public SearchResultsViewModel()
        {
            SearchResultsList = new List<SearchResultItemViewModel>();
            Page = "1";
        }
    }
}