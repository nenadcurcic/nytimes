using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NYTimesSearch.ViewModels
{
    public class SearchResultItemViewModel
    {
        /// <summary>
        /// Article title
        /// </summary>
        public string ArticleName { get; set; }
        
        /// <summary>
        /// Article link
        /// </summary>
        public string ArticleLink { get; set; }

        /// <summary>
        /// Article source
        /// </summary>
        public string ArticleSource { get; set; }
    }

    /// <summary>
    /// ViewModel representing search results
    /// </summary>
    public class SearchResultsViewModel
    {
        /// <summary>
        /// List of search result items
        /// </summary>
        public List<SearchResultItemViewModel> SearchResultsList { get; set; }

        /// <summary>
        /// Current page for pagination
        /// </summary>
        public string Page { get; set; }

        /// <summary>
        /// Searched item
        /// </summary>
        [Required]
        [Display(Name = "Search item")]
        public string SearchItem { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public SearchResultsViewModel()
        {
            SearchResultsList = new List<SearchResultItemViewModel>();
            Page = "1";
        }
    }
}