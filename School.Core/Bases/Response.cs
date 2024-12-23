﻿using System.Net;

namespace School.Core.Bases;
public class Response<T>
{
    public Response()
    {
    }
    public Response(T data,string? message = null)
    {
        Data = data;
        Message=message;
    }
    public Response(string message)
    {
        Message=message;
    }
    public Response(string message , bool succeeded)
    {
        Message = message;
        Succeeded=succeeded;
    }
    public bool Succeeded { get; set; }
    public string? Message { get; set; }
    public T Data { get; set; }

    public HttpStatusCode StatusCode { get; set; }
    public object? Meta { get; set; }
    public List<string> Errors { get; set; }
}
