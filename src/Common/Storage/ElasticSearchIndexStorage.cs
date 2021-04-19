using System;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using Nest;
namespace Common.Storage
{
    /// <summary>
    /// Elastic Search Abstraction. Storage used for storing search result indexes.
    /// </summary>
    public class ElasticSearchIndexStorage : IIndexStorage
    {
        ElasticClient _client;

        public ElasticSearchIndexStorage(IOptions<ElasticSearchConfiguration> elasticSearchOptions)
        {
            var settings = new ConnectionSettings(new Uri(elasticSearchOptions.Value.Uri)).DefaultIndex(elasticSearchOptions.Value.DefaultIndex);
            _client = new ElasticClient(settings);
        }

        /// <summary>
        /// Adds new search index to elastic search
        /// </summary>
        /// <param name="index"></param>
        public void Add(SearchIndex index)
        {
            var existing = Find(index.Url);
            if (existing == null)
            {
                _client.IndexDocument(index);
            }
        }

        /// <summary>
        /// Finds search index by url
        /// </summary>
        /// <param name="Url">Url</param>
        /// <returns>Found search index. Null when not found</returns>
        public SearchIndex Find(string Url)
        {
            var searchResponse = _client.Get<SearchIndex>(Url);
            if (!searchResponse.Found) return null;
            return searchResponse.Source;
        }

        /// <summary>
        /// Looks for indexes matching the query
        /// </summary>
        /// <param name="query">String query to find</param>
        /// <param name="skip">Skips x number of results. Used for pagination</param>
        /// <param name="take">Takes x number of results. Used for pagination</param>
        /// <param name="total">Total number of search indexes matching the query</param>
        /// <returns></returns>
        public IEnumerable<SearchIndex> Search(string query, int skip, int take, out int total)
        {
            var result = _client.Search<SearchIndex>(s => s.Query(q => q.MultiMatch(m => m.Query(query).Fields(f => f.Field(p => p.Title).Field(p => p.ShortDescription).Field(p => p.Content).Field(p => p.Url)))).Skip(skip).Size(take));
            total = Convert.ToInt32(result.Total);
            return result.Documents;
        }

        /// <summary>
        /// Updates existing index in storage
        /// </summary>
        /// <param name="index"></param>
        public void Update(SearchIndex index)
        {
            var old = Find(index.Url);
            if (old != null)
            {
                index.Created = old.Created;
                index.LastUpdate = DateTime.UtcNow;
                _client.Update<SearchIndex>(index.Url, (u => u.Doc(index)
                ));
            }
            else
            {
                throw new Exception("Cannot update record that does not exist");
            }
        }
    }
}
