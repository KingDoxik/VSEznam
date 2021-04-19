using System;
using System.Collections.Generic;

namespace Common.Storage
{
    public class InMemoryIndexStorage : IIndexStorage
    {
        public Dictionary<string, SearchIndex> db = new Dictionary<string, SearchIndex>();

        public void Add(SearchIndex index)
        {
            var existing = Find(index.Url);
            if (existing == null)
            {
                db.Add(index.Url, index);
            }
        }

        public SearchIndex Find(string Url)
        {
            if (db.ContainsKey(Url))
            {
                return db[Url];
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<SearchIndex> Search(string query, int skip, int take, out int total)
        {
            total = 0;
            return new List<SearchIndex>();
        }

        public void Update(SearchIndex index)
        {
            var old = Find(index.Url);
            if (old != null)
            {
                index.Created = old.Created;
                index.LastUpdate = DateTime.UtcNow;
                db[index.Url] = index;
            } else
            {
                throw new Exception("Cannot update record that does not exist");
            }
        }
    }
}
