using System;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Common.Queue;
using Common.Storage;
using Crawler.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Crawler
{
    public class CrawlerService : BackgroundService
    {
        private readonly IIndexingQueue _queue;
        private readonly IScrapperService _scrapperService;
        private readonly IIndexStorage _indexStorage;
        private readonly IConfiguration _configuration;
        private readonly bool _sameDomain;

        public CrawlerService(IIndexingQueue queue, IScrapperService scrapperService, IIndexStorage indexStorage, IConfiguration configuration)
        {
            _queue = queue;
            _scrapperService = scrapperService;
            _indexStorage = indexStorage;
            _configuration = configuration;
            _sameDomain = configuration.GetValue<bool>("SameDomain");
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            StartCrawler();
            return Task.CompletedTask;
        }

        /// <summary>
        /// Starts listening to queue. When new url is added to queue, starts scrapper. All found urls on the page adds back to the queue
        /// </summary>
        private void StartCrawler()
        {
            _queue.StartListening((url) =>
            {
                Uri currentUri;
                try {
                  currentUri = new Uri(url);
                } catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                    return;
                }

                var existingIndex = _indexStorage.Find(url);

                // We wont update if it is not required
                if (existingIndex != null && existingIndex.LastUpdate.AddMinutes(30) > DateTime.UtcNow) return;

                try
                {
                    var scrapperResult = _scrapperService.ScrapeAsync(url).Result;
                    if (scrapperResult.IsSuccess)
                    {
                        if (existingIndex == null) _indexStorage.Add(scrapperResult.SearchIndex);
                        else _indexStorage.Update(scrapperResult.SearchIndex);

                        foreach (Uri uri in scrapperResult.SearchIndex.ContainedUrls)
                        {
                            if (!_sameDomain || currentUri.Host == uri.Host)
                            {
                                _queue.Add(uri.ToString());
                            }
                        }
                    }

                    Console.WriteLine("Scrapping success");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Scrapping failed, " + e.Message);
                }
            });

            Console.WriteLine("Crawler started");
        }
    }
}
