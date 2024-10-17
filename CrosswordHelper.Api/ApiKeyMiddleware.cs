using CrosswordHelper.Api.Models;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace CrosswordHelper.Api
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IApiKeyValidation _apiKeyValidation;
        private readonly ApiKeySettings _settings;
        private readonly ILogger<ApiKeyMiddleware> _logger;

        public ApiKeyMiddleware(RequestDelegate next, IApiKeyValidation apiKeyValidation, ApiKeySettings settings, ILogger<ApiKeyMiddleware> logger)
        {
            _next = next;
            _apiKeyValidation = apiKeyValidation;
            _settings = settings;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (_settings.IsEnabled)
            {
                if (!ValidateApiKey(context, _settings))
                {
                    _logger.LogWarning("Unauthorised request attempted.");
                    return;
                }
            }

            await _next(context);
        }

        private bool ValidateApiKey(HttpContext context, ApiKeySettings settings)
        {
            if (context.Request.Path.Value.Contains("swagger")) return true;

            var apiKeyHeader = context.Request.Headers[ApiKeyValidation.ApiKeyHeaderName];

            if (string.IsNullOrWhiteSpace(apiKeyHeader))
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return false;
            }

            if (!_apiKeyValidation.IsValidApiKey(apiKeyHeader!))
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return false;
            }

            return true;
        }
    }
}
