namespace CustomerProfileCenter.Application.Response;

public class Response<T>
{
    public Response(T response)
    {
        Content = response;
        Error = new ResponseError();
    }

    public Response(string errorMessage)
    {
        Error = new ResponseError(errorMessage);
    }

    public T Content { get; set; }
    public ResponseError Error { get; set; }
}

public record ResponseError
{
    public ResponseError()
    {
    }

    public ResponseError(string errorMessage)
    {
        ErrorMessage = errorMessage;
    }

    public string ErrorMessage { get; } = string.Empty;

    public bool HasError => string.IsNullOrEmpty(ErrorMessage) is false;
}