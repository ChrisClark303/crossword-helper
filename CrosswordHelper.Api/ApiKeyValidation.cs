using CrosswordHelper.Api.Models;

namespace CrosswordHelper.Api
{
    public class ApiKeyValidation : IApiKeyValidation
    {
        public const string ApiKeyHeaderName = "X-Api-Key";
        private readonly ApiKeySettings _apiKeySettings;

        public ApiKeyValidation(ApiKeySettings settings)
        {
            _apiKeySettings = settings;
        }

        public bool IsValidApiKey(string userApiKey)
        {
            if (!_apiKeySettings.IsEnabled) return true;

            if (string.IsNullOrWhiteSpace(userApiKey))
            {
                return false;
            }

            if (_apiKeySettings.KeyValue != userApiKey)
            {
                return false;
            }

            return true;
        }
    }
}
