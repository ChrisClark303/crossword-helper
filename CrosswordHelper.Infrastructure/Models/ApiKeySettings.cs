namespace CrosswordHelper.Infrastructure.Models
{
    public class ApiKeySettings : IApiKeySettings
    {
        public bool IsEnabled { get; set; }
        public string KeyValue { get; set; }
    }
}
