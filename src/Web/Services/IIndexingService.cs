using System;
namespace Web.Services
{
    public interface IIndexingService
    {
        /// <summary>
        /// Adds page to indexing queue to be scraped and store in index storage
        /// </summary>
        /// <param name="url"></param>
        public void IndexPage(string url);
    }
}
