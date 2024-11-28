using DemoCICD.Contract.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoCICD.Presentation.Abstractions;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public abstract class ApiController : ControllerBase
{
    protected readonly ISender Sender;

    protected ApiController(ISender sender) => Sender = sender;

    /// <summary>
    /// Xử lí thất bại.
    /// </summary>
    /// <param name="result">Thông tin lỗi.</param>
    /// <returns>IActionResult.</returns>
    /// <exception cref="InvalidOperationException"></exception>
    protected IActionResult HandlerFailure(Result result) =>
    result switch
    {
        { IsSuccess: true } => throw new InvalidOperationException(),
        IValidationResult validationResult =>
            BadRequest(
                CreateProblemDetails(
                    "Validation Error",
                    StatusCodes.Status400BadRequest,
                    result.Error,
                    validationResult.Errors)),
        _ =>
            BadRequest(
                CreateProblemDetails(
                    "Bab Request",
                    StatusCodes.Status400BadRequest,
                    result.Error))
    };

    /// <summary>
    /// Chi tiết các thông lỗi được trả ra.
    /// </summary>
    /// <param name="title">Tiêu đề lỗi.</param>
    /// <param name="status">Trạng thái lỗi.</param>
    /// <param name="error">Thông tin lỗi.</param>
    /// <param name="errors">Danh sách các thông báo lỗi.</param>
    /// <returns>ProblemDetails.</returns>
    private static ProblemDetails CreateProblemDetails(
        string title,
        int status,
        Error error,
        Error[]? errors = null) =>
        new()
        {
            Title = title,
            Type = error.Code,
            Detail = error.Message,
            Status = status,
            Extensions = { { nameof(errors), errors } }
        };
}
