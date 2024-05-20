namespace Microservice.Product.Application.Commons.Bases;

public class BaseResponse<T>
{
    public bool IsSuccess { get; set; }
    public T? Data { get; set; }
    public string Message { get; set; } = null!;
    public IEnumerable<BaseError>? Errors { get; set; }
    public int? TotalRecords { get; set; }
}