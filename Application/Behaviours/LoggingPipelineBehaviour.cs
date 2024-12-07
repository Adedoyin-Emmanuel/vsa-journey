using FluentResults;
using MediatR;


namespace vsa_journey.Infrastructure.Behaviours;

public class LoggingPipelineBehaviour<TRequest, TResponse>(
    ILogger<LoggingPipelineBehaviour<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result
{

    private readonly ILogger<LoggingPipelineBehaviour<TRequest, TResponse>> _logger = logger;

    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
