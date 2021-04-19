using System;
namespace Common.Storage
{

    /// <summary>
    /// Elastic Search configuration POCO
    /// </summary>
    public class ElasticSearchConfiguration
    {
        /// <summary>
        /// Elastic Search Uri
        /// </summary>
        public string Uri { get; set; }

        /// <summary>
        /// Default index name
        /// </summary>
        public string DefaultIndex { get; set; }
    }
}
