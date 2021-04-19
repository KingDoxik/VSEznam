using System;
using Common.Storage;

namespace Crawler.Services
{
    /// <summary>
    /// Result of scrapping
    /// </summary>
    public class ScrapperResult
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public int HttpStatusCode { get; set; }
        public SearchIndex SearchIndex { get; set; }
    }
}
