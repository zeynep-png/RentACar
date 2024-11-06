﻿using System.Net;
using System.Text.Json;

namespace RentACar.WebApi.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        
    private readonly RequestDelegate _next;
        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var response = new
            {
                StatusCode = context.Response.StatusCode,
                Message = "Server Error. Try Again.",
                Detailed = exception.Message 
            };
            var jsonResponse = JsonSerializer.Serialize(response);
            return context.Response.WriteAsync(jsonResponse);
        }
    }
}

