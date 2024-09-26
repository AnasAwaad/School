namespace School.Core.Bases;
public class ResponseHandler
{
    public ResponseHandler()
    {

    }
    public Response<T> Delete<T>()
    {
        return new Response<T>
        {
            StatusCode = System.Net.HttpStatusCode.OK,
            Succeeded = true,
            Message = "Deleted Successfully"
        };
    }

    public Response<T> Success<T>(T entity, object? meta = null)
    {
        return new Response<T>
        {
            Data = entity,
            Meta = meta,
            StatusCode = System.Net.HttpStatusCode.OK,
            Succeeded = true,
        };
    }

    public Response<T> Unauthorize<T>()
    {
        return new Response<T>
        {
            StatusCode = System.Net.HttpStatusCode.Unauthorized,
            Succeeded = true,
            Message = "Unauthorize"
        };
    }

    public Response<T> BadRequest<T>(string? message = null)
    {
        return new Response<T>
        {
            StatusCode = System.Net.HttpStatusCode.BadRequest,
            Succeeded = false,
            Message = message is null ? "Bad Request" : message
        };
    }

    public Response<T> UnprocessableEntity<T>(string? message = null)
    {
        return new Response<T>
        {
            StatusCode = System.Net.HttpStatusCode.UnprocessableEntity,
            Succeeded = false,
            Message = message is null ? "Unprocessable Entity" : message
        };
    }

    public Response<T> NotFound<T>(string? message = null)
    {
        return new Response<T>
        {
            StatusCode = System.Net.HttpStatusCode.NotFound,
            Succeeded = false,
            Message = message is null ? "Not Found" : message
        };
    }

    public Response<T> Created<T>(T entity, object? meta = null)
    {
        return new Response<T>
        {
            Data = entity,
            StatusCode = System.Net.HttpStatusCode.Created,
            Succeeded = true,
            Message = "Created",
            Meta = meta
        };
    }

}
