using ApiConsumer.Messages;
using MediatR;

namespace ApiConsumer.Handlers
{
    public class GustomerDeletedHandler : IRequestHandler<CustomerDeleted>
    {
        private readonly ILogger<GustomerDeletedHandler> _logger;

        public GustomerDeletedHandler(ILogger<GustomerDeletedHandler> logger)
        {
            _logger = logger;
        }

        public Task<Unit> Handle(CustomerDeleted request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GustomerDeletedHandler - {request.Id.ToString()}");
            return Unit.Task;
        }
    }
}
