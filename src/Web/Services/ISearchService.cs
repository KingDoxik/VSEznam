using System;
using System.Collections.Generic;
using Web.Models;

namespace Web.Services
{
    public interface ISearchService
    {
        /// <summary>
        /// Searches the database for results matching the query
        /// </summary>
        /// <param name="query">Text to find</param>
        /// <param name="page">Page number (default 1)</param>
        /// <param name="pageSize">Number of results per page</param>
        /// <returns></returns>
        SearchResult Search(string query, int page, int pageSize);
    }
}
