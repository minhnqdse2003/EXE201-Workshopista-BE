using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Serilog;
using Serilog.Context;
using System;

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
                    _logger.LogWarning($"{correlationId} processing.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(
                    ex, "Exception occurred: {Message}", ex.Message);

                    var problemDetails = new ProblemDetails
                    {
                        Status = StatusCodes.Status500InternalServerError,
                        Title = "Server Error"
                    };

                    httpContext.Response.StatusCode =
                        StatusCodes.Status500InternalServerError;
                }
            }
        }

        private static string GetCorrelationId(HttpContext context)
        {
            context.Request.Headers.TryGetValue(
                CorrelationIdHeaderName, out StringValues correlationId);

            return correlationId.FirstOrDefault() ?? context.TraceIdentifier;
        }
    }

}
