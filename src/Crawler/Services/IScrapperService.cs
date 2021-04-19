using System;
using System.Threading.Tasks;
using Common.Storage;

namespace Crawler.Services
{
    public interface IScrapperService
    {
        /// <summary>
        /// Gets data from the url and extracts all important information and saves in in Index Storage
        /// </summary>
        /// <param name="url">Url to be scaped</param>
        /// <returns></returns>
        Task<ScrapperResult> ScrapeAsync(string url); 
    }
}
