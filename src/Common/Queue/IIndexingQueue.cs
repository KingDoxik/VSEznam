using System;
namespace Common.Queue
{
    /// <summary>
    /// Queue used for storing URLs that should be parsed
    /// </summary>
    public interface IIndexingQueue
    {
        /// <summary>
        /// Adds new url to queue
        /// </summary>
        /// <param name="url">Url to be added</param>
        void Add(string url);

        /// <summary>
        /// Listens to new Urls added to queue
        /// </summary>
        /// <param name="onIndexAdded">Action, that will be called when new item is added</param>
        void StartListening(Action<string> onIndexAdded);
    }
}
