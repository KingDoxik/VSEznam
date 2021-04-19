using System;

namespace Common.Queue
{
    /// <summary>
    /// Rabbit Mq configuration file
    /// </summary>
    public class RabbitMqConfiguration
    {
        /// <summary>
        /// RabbitMq Hostname
        /// </summary>
        public string Hostname { get; set; }

        /// <summary>
        /// RabbitMq Username
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// RabbitMq Password
        /// </summary>
        public string Password { get; set; }
    }
}
