using Amazon.SQS.Model;

namespace Api.Messaging
{
    public interface ISqsMessenger
    {
        Task<SendMessageResponse> SendMessageAsync<T>(T message);
    }
}
