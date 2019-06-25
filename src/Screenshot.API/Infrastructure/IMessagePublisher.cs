namespace Screenshot.API.Infrastructure
{
    public interface IMessagePublisher
    {
        void Publish(string url);
    }
}