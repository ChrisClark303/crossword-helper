namespace CrosswordHelper.Api
{
    public interface IApiKeyValidation
    {
        bool IsValidApiKey(string userApiKey);
    }
}