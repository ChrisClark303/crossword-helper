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

        public ApiKeyMiddleware(RequestDelegate next, IApiKeyValidation apiKeyValidation, ApiKeySettings settings)
        {
            _next = next;
            _apiKeyValidation = apiKeyValidation;
            _settings = settings;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (_settings.IsEnabled)
            {
                if (!ValidateApiKey(context, _settings)) return;
            }

            await _next(context);
        }

        private bool ValidateApiKey(HttpContext context, ApiKeySettings settings)
        {
            if (context.Request.Path.Value.Contains("swagger")) return true;

            if (string.IsNullOrWhiteSpace(context.Request.Headers[ApiKeyValidation.ApiKeyHeaderName]))
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return false;
            }

            string? userApiKey = context.Request.Headers[ApiKeyValidation.ApiKeyHeaderName];

            if (!_apiKeyValidation.IsValidApiKey(userApiKey!))
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return false;
            }

            return true;
        }
    }
}
