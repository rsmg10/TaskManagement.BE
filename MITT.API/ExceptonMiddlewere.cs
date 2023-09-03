using System.Net;
using MITT.EmployeeDb.Models;

namespace MITT.API;

public class ExceptonMiddlewere
{
    private readonly RequestDelegate _next;

    public ExceptonMiddlewere(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            var response = e switch
            {
                ApplicationException _ => new Errors.ExceptionResponse(HttpStatusCode.BadRequest, "Application exception occurred."),
                KeyNotFoundException _ => new Errors.ExceptionResponse(HttpStatusCode.NotFound, "The request key not found."),
                UnauthorizedAccessException _ => new Errors.ExceptionResponse(HttpStatusCode.Unauthorized, "Unauthorized."),
                _ => new Errors.ExceptionResponse(HttpStatusCode.InternalServerError, "Internal server error. Please retry later.")
            };
            
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)response.StatusCode;
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}