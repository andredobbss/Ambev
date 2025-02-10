namespace Ambev.Application.Shared;

public class ApiErrorResponse
{
    public string Type { get; set; }
    public string Error { get; set; }
    public string Detail { get; set; }
    public object Errors { get; set; }

    public ApiErrorResponse(string type, string error, string detail, object errors = null)
    {
        Type = type;
        Error = error;
        Detail = detail;
        Errors = errors;
    }
}
