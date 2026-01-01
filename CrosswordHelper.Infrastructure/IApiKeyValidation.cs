namespace CrosswordHelper.Infrastructure
{
    public interface IApiKeyValidation
    {
        bool IsValidApiKey(string userApiKey);
    }
}