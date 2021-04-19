using System;
using System.Collections.Generic;
using Common.Storage;
using Web.Models;

namespace Web.Services
{
    public class SearchService : ISearchService
    {
        private readonly IIndexStorage _indexStorage;

        public SearchService(IIndexStorage indexStorage)
        {
            _indexStorage = indexStorage;
        }

        /// <summary>
        /// Searches the database for results matching the query
        /// </summary>
        /// <param name="query">Text to find</param>
        /// <param name="page">Page number (default 1)</param>
        /// <param name="pageSize">Number of results per page</param>
        /// <returns></returns>
        public SearchResult Search(string query, int page, int pageSize)
        {
            int total = 0;
            page = page < 1 ? 1 : page;
            var searchIndexes = _indexStorage.Search(query, (page - 1) * pageSize, pageSize, out total);
            var searchResult = new SearchResult
            {
                CurrentPage = page,
                ItemsPerPage = pageSize,
                NumberOfPages = (int)Math.Floor((double)total / (double)pageSize) + 1,
                Total = total
            };

            var searchResultItems = new List<SearchResultItem>();

            foreach (var searchIndex in searchIndexes)
            {
                searchResultItems.Add(new SearchResultItem
                {
                    Url = searchIndex.Url,
                    Title = searchIndex.Title,
                    ShortDescription = searchIndex.ShortDescription
                });
            }

            searchResult.Items = searchResultItems;
            return searchResult;
        }
    }
}
