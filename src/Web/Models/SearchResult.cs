using System;
using System.Collections.Generic;

namespace Web.Models
{
    public class SearchResult
    {
        public string Query { get; set; }
        public int Total { get; set; }
        public int CurrentPage { get; set; }
        public int ItemsPerPage { get; set; }
        public int NumberOfPages { get; set; }
        public IEnumerable<SearchResultItem> Items { get; set; }
    }
}
