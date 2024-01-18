using PixelPlanner.Persistence;
using PixelPlanner.UseCases;

namespace PixelPlanner.Api;

public static class ServiceRegistrations
{
    public static IServiceCollection AddAppServices(this IServiceCollection serviceCollection) =>
        serviceCollection
            .AddTransient<IGridService, GridService>()
            .AddTransient<IGridRepository, InMemoryGridRepository>();
}
