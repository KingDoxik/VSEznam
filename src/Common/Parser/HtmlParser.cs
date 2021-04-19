using System;
using HtmlAgilityPack;
using System.Linq;
using Common.Helpers;

namespace Common.Parser
{
    /// <summary>
    /// Html parser that extracts important information from html files
    /// </summary>
    public class HtmlParser
    {
        /// <summary>
        /// Parses html string and return important information from the html as ParserResult
        /// </summary>
        /// <param name="htmlContent">Html string to be parsed</param>
        /// <returns>Result</returns>
        public ParserResult Parse(string htmlContent)
        {
            var result = new ParserResult();

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(htmlContent);

            // Parse data from head
            var head = htmlDocument.DocumentNode.Descendants("head").FirstOrDefault();
            if (head != null)
            {
                // Parse title
                var title = head.Descendants("title").FirstOrDefault();
                if (title != null)
                {
                    result.Title = title.InnerText;
                }
                else
                {
                    var h1 = htmlDocument.DocumentNode.Descendants("h1").FirstOrDefault();
                    if (h1 != null)
                    {
                        result.Title = h1.InnerText.StripHtmlTags();
                    }
                    else
                    {
                        result.Title = null;
                    }
                }

                // Parse meta
                var metaTags = head.Descendants("meta");
                foreach (var metaTag in metaTags)
                {
                    var metaName = metaTag.GetAttributeValue("name", null);
                    var metaContent = metaTag.GetAttributeValue("content", null);
                    if (metaName != null && metaContent != null)
                    {
                        result.Metadata[metaName] = metaContent;
                    }
                }
            }

            var body = htmlDocument.DocumentNode.Descendants("body").FirstOrDefault();
            if (body != null)
            {
                var linkTags = htmlDocument.DocumentNode.Descendants("a");
                foreach (var linkTag in linkTags)
                {
                    var hrefAttribute = linkTag.GetAttributes("href").FirstOrDefault();
                    if (hrefAttribute != null)
                    {
                        try
                        {
                            var uri = new Uri(hrefAttribute.Value);
                            result.ContainedUrls.Add(uri);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }

                }

                // Remove non-text content
                htmlDocument.DocumentNode.Descendants()
                    .Where(n => n.Name == "script" || n.Name == "style" || n.Name == "img" || n.Name == "iframe" || n.Name == "canvas")
                    .ToList()
                    .ForEach(n => n.Remove());

                result.ParsedContent = body.InnerHtml.StripHtmlTags();
                result.ShortDescription = result.ParsedContent.Length >= 400 ? result.ParsedContent.Substring(0, 400) : result.ParsedContent;
            }

            if (result.Metadata.ContainsKey("description") && result.Metadata["description"] != null)
            {
                result.ShortDescription = result.Metadata["description"].Length >= 400 ? result.Metadata["description"].Substring(0, 400) : result.Metadata["description"];
            }

            return result;
        }
    }
}
