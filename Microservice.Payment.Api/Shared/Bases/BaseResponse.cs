using FluentValidation.Results;

namespace Microservice.Payment.Api.Shared.Bases;

public class BaseResponse<T>
{
    public bool IsSuccess { get; set; }
    public T? Data { get; set; }
    public string Message { get; set; } = string.Empty;
    public IEnumerable<ValidationFailure>? Errors { get; set; }
}