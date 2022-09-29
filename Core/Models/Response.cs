namespace NotasAPI.Core.Models;

public class Response<T>
{
    public Response()
    {
    }

    public Response(T data, bool succeeded = true, string[] errors = null, string message = "Success")
    {
        Data = data;
        Succeeded = succeeded;
        Errors = errors;
        Message = message;
    }

    public T Data { get; set; }
    public bool Succeeded { get; set; }
    public string[] Errors { get; set; }
    public string Message { get; set; }
}

public class ResponseBase
{
    public object Data { get; set; }
    public bool Succeeded { get; set; }
    public Dictionary<string, IEnumerable<string>> Errors { get; set; }
    public string Message { get; set; }
}
public static class ResponseMessage
{
    public const string Success = "Success";
    public const string Error = "Error";
    public const string NotFound = "Record not found";
    public const string ValidationErrors = "Validations errors found";
    public const string UnexpectedErrors = "An unexpected error occurred, try again later";
    public const string InvalidModelStateMessage = "The modelState is invalid, check if you have the model right.";
}
