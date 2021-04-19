using System;
using Common.Queue;

namespace Web.Services
{
    public class IndexingService : IIndexingService
    {
        private readonly IIndexingQueue _indexingQueue;

        public IndexingService(IIndexingQueue indexingQueue)
        {
            _indexingQueue = indexingQueue;
        }

        /// <summary>
        /// Adds page to indexing queue to be scraped and store in index storage
        /// </summary>
        /// <param name="url"></param>
        public void IndexPage(string url)
        {
            _indexingQueue.Add(url);
        }
    }
}
