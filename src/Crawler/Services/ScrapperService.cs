using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Common.Parser;
using Common.Storage;

namespace Crawler.Services
{
    public class ScrapperService : IScrapperService
    {
        HtmlParser _htmlParser;
        HttpClient _client;

        public ScrapperService()
        {
            _htmlParser = new HtmlParser();
            _client = new HttpClient();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls13;
        }

        /// <summary>
        /// Gets data from the url and extracts all important information and saves in in Index Storage
        /// </summary>
        /// <param name="url">Url to be scaped</param>
        /// <returns></returns>
        public async Task<ScrapperResult> ScrapeAsync(string url)
        {
            Console.WriteLine("Started scrapping: " + url);
            try
            {
                var html = await _client.GetStringAsync(url);
                var parserResult = _htmlParser.Parse(html);
                var searchIndex = new SearchIndex
                {
                    Title = parserResult.Title ?? url,
                    ShortDescription = parserResult.ShortDescription,
                    ContainedUrls = parserResult.ContainedUrls,
                    Metadata = parserResult.Metadata,
                    Url = url,
                    Content = parserResult.ParsedContent
                };

                var scrapperResult = new ScrapperResult
                {
                    IsSuccess = true,
                    SearchIndex = searchIndex
                };

                return scrapperResult;
            }
            catch (HttpRequestException e)
            {
                var result = new ScrapperResult
                {
                    IsSuccess = false,
                    ErrorMessage = e.Message
                };
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine("En error occured during data fetching from url " + url + ", " + e.Message);
                var result = new ScrapperResult
                {
                    IsSuccess = false,
                    ErrorMessage = e.Message
                };
                return result;
            }
        }
    }
}
