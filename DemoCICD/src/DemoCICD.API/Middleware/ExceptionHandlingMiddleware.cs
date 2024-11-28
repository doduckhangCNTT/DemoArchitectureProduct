using DemoCICD.Domain.Exceptions;
using System.Text.Json;

namespace DemoCICD.API.Middleware;

internal sealed class ExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
        => _logger = logger;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);

            await HandleExceptionAsync(context, e);
        }
    }

    /// <summary>
    /// Xử lí lỗi ngoại lệ.
    /// </summary>
    /// <param name="httpContext"></param>
    /// <param name="exception">Thông tin ngoại lệ.</param>
    /// <returns></returns>
    private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        var statusCode = GetStatusCode(exception);

        var response = new
        {
            title = GetTitle(exception),
            status = statusCode,
            detail = exception.Message,
            errors = GetErrors(exception),
        };

        httpContext.Response.ContentType = "application/json";

        httpContext.Response.StatusCode = statusCode;

        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
    }

    /// <summary>
    /// Lấy trạng thái code (200, 404...)
    /// </summary>
    /// <param name="exception">Ngoại lệ.</param>
    /// <returns>Mã lỗi.</returns>
    private static int GetStatusCode(Exception exception) =>
        exception switch
        {
            BadRequestException => StatusCodes.Status400BadRequest,
            NotFoundException => StatusCodes.Status404NotFound,
            //Application.Exceptions.ValidationException => StatusCodes.Status422UnprocessableEntity,
            FluentValidation.ValidationException => StatusCodes.Status400BadRequest,
            FormatException => StatusCodes.Status422UnprocessableEntity,
            _ => StatusCodes.Status500InternalServerError
        };

    /// <summary>
    /// Lấy tiêu đề lỗi.
    /// </summary>
    /// <param name="exception">Ngoại lệ.</param>
    /// <returns>Tiêu đề lỗi.</returns>
    private static string GetTitle(Exception exception) =>
        exception switch
        {
            DomainException applicationException => applicationException.Title,
            _ => "Server Error"
        };

    /// <summary>
    /// Lấy tất cả thông tin lỗi.
    /// </summary>
    /// <param name="exception">Ngoại lệ.</param>
    /// <returns>Thông tin các lỗi.</returns>
    private static IReadOnlyCollection<Application.Exceptions.ValidationError> GetErrors(Exception exception)
    {
        IReadOnlyCollection<Application.Exceptions.ValidationError> errors = null;

        if (exception is Application.Exceptions.ValidationException validationException)
        {
            errors = validationException.Errors;
        }

        return errors;
    }
}
