using Microsoft.Extensions.Localization;
using School.Core.Resources;

namespace School.Core.Bases;
public class ResponseHandler
{
    private readonly IStringLocalizer<SharedResources> _localizer;
    public ResponseHandler(IStringLocalizer<SharedResources> localizer)
    {
        _localizer = localizer;
    }
    public Response<T> Delete<T>()
    {
        return new Response<T>
        {
            StatusCode = System.Net.HttpStatusCode.OK,
            Succeeded = true,
            Message = _localizer[SharedResourcesKeys.Deleted]
        };
    }

    public Response<T> Success<T>(T entity, object? meta = null, string? message = null)
    {
        return new Response<T>
        {
            Data = entity,
            Meta = meta,
            StatusCode = System.Net.HttpStatusCode.OK,
            Succeeded = true,
            Message = message ?? _localizer[SharedResourcesKeys.Success]
        };
    }

    public Response<T> Unauthorize<T>()
    {
        return new Response<T>
        {
            StatusCode = System.Net.HttpStatusCode.Unauthorized,
            Succeeded = true,
            Message = _localizer[SharedResourcesKeys.Unauthorize]
        };
    }

    public Response<T> BadRequest<T>(string? message = null)
    {
        return new Response<T>
        {
            StatusCode = System.Net.HttpStatusCode.BadRequest,
            Succeeded = false,
            Message = message is null ? _localizer[SharedResourcesKeys.BadRequest] : message
        };
    }

    public Response<T> UnprocessableEntity<T>(string? message = null)
    {
        return new Response<T>
        {
            StatusCode = System.Net.HttpStatusCode.UnprocessableEntity,
            Succeeded = false,
            Message = message is null ? _localizer[SharedResourcesKeys.Unauthorize] : message
        };
    }

    public Response<T> NotFound<T>(string? message = null)
    {
        return new Response<T>
        {
            StatusCode = System.Net.HttpStatusCode.NotFound,
            Succeeded = false,
            Message = message is null ? _localizer[SharedResourcesKeys.NotFound] : message
        };
    }

    public Response<T> Created<T>(T entity, object? meta = null)
    {
        return new Response<T>
        {
            Data = entity,
            StatusCode = System.Net.HttpStatusCode.Created,
            Succeeded = true,
            Message = _localizer[SharedResourcesKeys.Created],
            Meta = meta
        };
    }

}
