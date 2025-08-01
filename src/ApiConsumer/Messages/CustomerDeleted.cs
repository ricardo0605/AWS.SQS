namespace ApiConsumer.Messages
{
    public class CustomerDeleted : ISqsMessage
    {
        public required Guid Id { get; init; } = Guid.NewGuid();
    }
}
