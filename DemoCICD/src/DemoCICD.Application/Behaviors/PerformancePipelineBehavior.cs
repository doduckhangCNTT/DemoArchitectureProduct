using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace DemoCICD.Application.Behaviors;

/// <summary>
/// Xử lí performance.
/// </summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public class PerformancePipelineBehavior<TRequest, TResponse> :
    IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly Stopwatch _timer;
    private readonly ILogger<TRequest> _logger;

    public PerformancePipelineBehavior(ILogger<TRequest> logger)
    {
        _timer = new Stopwatch();
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _timer.Start();
        var response = await next();
        _timer.Stop();

        var elapsedMilliseconds = _timer.ElapsedMilliseconds;

        // Nếu <= 5000 ms hàm handler thỏa mãn yêu cầu về time thực thi.
        if (elapsedMilliseconds <= 5000)
            return response;

        // Vượt quá 5000 ms thì log ra để thông báo (hàm handle xử lí mất nhiều thời gian)
        var requestName = typeof(TRequest).Name;
        _logger.LogWarning("Long Time Running - Request Details: {Name} ({ElapsedMilliseconds} milliseconds) {@Request}",
            requestName, elapsedMilliseconds, request);

        return response;
    }
}
