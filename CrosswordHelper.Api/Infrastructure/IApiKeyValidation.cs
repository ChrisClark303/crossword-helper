namespace CrosswordHelper.Api.Infrastructure
{
    public interface IApiKeyValidation
    {
        bool IsValidApiKey(string userApiKey);
    }
}