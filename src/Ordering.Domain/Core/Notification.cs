namespace Ordering.Domain.Core
{
    public class Notification
    {
        public string Key { get; }
        public string Message { get; }

        public Notification(string key, string message)
        {
            this.Key = key;
            this.Message = message;
        }
    }
}