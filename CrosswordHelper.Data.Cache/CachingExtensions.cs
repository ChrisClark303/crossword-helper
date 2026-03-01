using CrosswordHelper.Data.Export;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

public static class CachingExtensions 
{
    public static void AddCaching(this IServiceCollection services)
    {
        services.AddScoped<ICrosswordDataCache, CrosswordDataFileBackedCache>();
        services.AddSingleton<IMemoryCache>(new MemoryCache(new MemoryCacheOptions()));
    }
}