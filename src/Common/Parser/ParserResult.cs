using System;
using System.Collections.Generic;

namespace Common.Parser
{
    /// <summary>
    /// POCO representation of parsed information from HTML file
    /// </summary>
    public class ParserResult
    {
        /// <summary>
        /// Html title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Html meta description or first few paragraphs of content
        /// </summary>
        public string ShortDescription { get; set; }

        /// <summary>
        /// Content stripped of html tags
        /// </summary>
        public string ParsedContent { get; set; }

        //public Metadata Metadata { get; set; } = new Metadata();
        /// <summary>
        /// Meta tags
        /// </summary>
        public Dictionary<string, string> Metadata { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// Urls used in the file
        /// </summary>
        public HashSet<Uri> ContainedUrls { get; set; } = new HashSet<Uri>();
    }
}
