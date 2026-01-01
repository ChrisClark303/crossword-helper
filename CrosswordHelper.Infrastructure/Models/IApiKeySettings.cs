namespace CrosswordHelper.Infrastructure.Models
{
    public interface IApiKeySettings
    {
        bool IsEnabled { get; set; }
        string KeyValue { get; set; }
    }
}