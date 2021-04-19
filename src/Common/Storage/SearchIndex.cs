using System;
using System.Collections.Generic;
using Nest;

namespace Common.Storage
{
    /// <summary>
    /// Search Index is object, that contains all the information about stored websites. It will be used for searching
    /// </summary>
    [ElasticsearchType(IdProperty = nameof(Url))]
    public class SearchIndex
    {
        public string Url { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public Dictionary<string, string> Metadata { get; set; } = new Dictionary<string, string>();
        public HashSet<Uri> ContainedUrls { get; set; } = new HashSet<Uri>();
        public string Content { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime LastUpdate { get; set; } = DateTime.UtcNow;
    }
}
