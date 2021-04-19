using System;
using System.Collections.Generic;

namespace Common.Storage
{
    /// <summary>
    /// Storage used for storing search result indexes.
    /// </summary>
    public interface IIndexStorage
    {
        /// <summary>
        /// Finds search index by url
        /// </summary>
        /// <param name="Url">Url</param>
        /// <returns>Found search index. Null when not found</returns>
        SearchIndex Find(string Url);

        /// <summary>
        /// Adds new search index to storage
        /// </summary>
        /// <param name="index"></param>
        void Add(SearchIndex index);

        /// <summary>
        /// Updates existing index in storage
        /// </summary>
        /// <param name="index"></param>
        void Update(SearchIndex index);

        /// <summary>
        /// Looks for indexes matching the query
        /// </summary>
        /// <param name="query">String query to find</param>
        /// <param name="skip">Skips x number of results. Used for pagination</param>
        /// <param name="take">Takes x number of results. Used for pagination</param>
        /// <param name="total">Total number of search indexes matching the query</param>
        /// <returns></returns>
        IEnumerable<SearchIndex> Search(string query, int skip, int take, out int total);
    }
}
