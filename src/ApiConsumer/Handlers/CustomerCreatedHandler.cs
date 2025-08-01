using ApiConsumer.Messages;
using MediatR;

namespace ApiConsumer.Handlers
{
    public class CustomerCreatedHandler : IRequestHandler<CustomerCreated>
    {
        private readonly ILogger<CustomerCreatedHandler> _logger;

        public CustomerCreatedHandler(ILogger<CustomerCreatedHandler> logger)
        {
            _logger = logger;
        }

        public Task<Unit> Handle(CustomerCreated request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CustomerCreatedHandler - {request.FullName}");
            return Unit.Task;
        }
    }
}
