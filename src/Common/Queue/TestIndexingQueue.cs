using System;
namespace Common.Queue
{
    public class TestIndexingQueue : IIndexingQueue
    {
        private Action<string> actionToCallWhenIndexAdded;

        public void Add(string url)
        {
            actionToCallWhenIndexAdded.Invoke(url);
            Console.WriteLine(url);
        }

        public void StartListening(Action<string> onIndexAdded)
        {
            actionToCallWhenIndexAdded = onIndexAdded;
        }
    }
}
