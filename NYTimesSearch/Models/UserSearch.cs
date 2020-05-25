using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NYTimesSearch.Models
{
    public class UserSearch
    {
        public int Id { get; set; }
        public string UserName { get; set; }

        [Display(Name = "Search news")]
        public string SearchItem { get; set; }
    }
}