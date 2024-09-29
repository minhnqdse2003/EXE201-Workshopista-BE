using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Repository.Helpers;
using Serilog;
using Serilog.Context;
using Service.Models;
using System;
using System.Net;
using System.Text.Json.Serialization;

namespace EXE201_Workshopista.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private const string CorrelationIdHeaderName = "X-Correlation-Id";

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            string correlationId = GetCorrelationId(httpContext);
            using (LogContext.PushProperty("CorrelationId", correlationId))
            {
                try
                {
                    await _next.Invoke(httpContext);
                }
                catch (Exception ex)
                {
                    var statusCode = ex is CustomException ? StatusCodes.Status400BadRequest : StatusCodes.Status500InternalServerError;
                    _logger.LogError(
                    ex, "Exception occurred: {Message}", ex.Message.ToString());

                    await HandleExceptionAsync(httpContext, ex, statusCode);
                }
            }
        }

        private static string GetCorrelationId(HttpContext context)
        {
            context.Request.Headers.TryGetValue(
                CorrelationIdHeaderName, out StringValues correlationId);

            return correlationId.FirstOrDefault() ?? context.TraceIdentifier;
        }

        private async static Task HandleExceptionAsync(HttpContext context, Exception exception, int statusCode)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            await context.Response.WriteAsJsonAsync(
                ApiResponse<string>.ErrorResponse(
                    ResponseMessage.InternalServerError,
                    new List<string> { exception.Message }
                )
            );
        }
    }

}
