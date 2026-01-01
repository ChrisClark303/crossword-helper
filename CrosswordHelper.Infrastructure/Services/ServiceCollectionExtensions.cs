using CrosswordHelper.Data;
using CrosswordHelper.Infrastructure.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CrosswordHelper.Infrastructure.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApiKey(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddConfigSection<IApiKeySettings, ApiKeySettings>(configuration, "ApiKeySettings");

            var apiKeySettings = configuration.GetSection("ApiKeySettings")
                .Get<ApiKeySettings>();

            services.AddSingleton(apiKeySettings!);
            services.AddTransient<IApiKeyValidation, ApiKeyValidation>();

            return services;
        }

        public static IServiceCollection AddConnectionStrings(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddConfigSection<IConnectionStrings, ConnectionStrings>(configuration, "ConnectionStrings");
            return services;
        }

        public static IServiceCollection AddConfigSection<TConfig, TConfigImpl>(
            this IServiceCollection services, IConfiguration configuration, string sectionName)
            where TConfig : class
            where TConfigImpl : TConfig
        {
            var config = configuration.GetSection(sectionName)
                .Get<TConfigImpl>();

            services.AddSingleton<TConfig>(config!);

            return services;
        }
    }
}
