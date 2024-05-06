using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Text.Json;

namespace Microservice.Product.Application.Commons.Behaviors;

public class PerformanceBehaviors<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly Stopwatch _stopwatch;
    private readonly ILogger<TRequest> _logger;

    public PerformanceBehaviors(ILogger<TRequest> logger)
    {
        _stopwatch = new Stopwatch();
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _stopwatch.Start();
        var response = await next();
        _stopwatch.Stop();

        var elapsedMilliseconds = _stopwatch.ElapsedMilliseconds;

        if (elapsedMilliseconds > 10)
        {
            var requestName = typeof(TRequest).Name;
            _logger
                .LogWarning("Microservice Product long running Request: {name}({elapsedMilliseconds} milliseconds) {@Request}", 
                requestName, elapsedMilliseconds, JsonSerializer.Serialize(request));
        }

        return response;
    }
}