namespace BookManagement.Api.Configurations;

public static partial class HostConfiguration
{
    public static ValueTask<WebApplicationBuilder> ConfigureAsync(this WebApplicationBuilder builder)
    {
        builder
            .AddSerializers()
            .AddMappers()
            .AddValidators()
            .AddCaching()
            .AddEventBus()
            .AddPersistence()
            .AddIdentityInfrastructure()
            .AddNotificationInfrastructure()
            .AddVerificationInfrastructure()
            .AddBooksInfrastructure()
            .AddMediatR()
            .AddReqeuestContextTools()
            .AddCors()
            .AddDevTools()
            .AddExposers();

        return new(builder);
    }

    public static async ValueTask<WebApplication> ConfigureAsync(this WebApplication app)
    {
        await app
            .MigratedataBaseSchemasAsync();

        await app.SeedDataAsync();
 
        app
            .UseCors();

        app
            .UseDevTools()
            .UseExposers()
            .UseIdentityInfrustructure();

        return app;
    }
}
