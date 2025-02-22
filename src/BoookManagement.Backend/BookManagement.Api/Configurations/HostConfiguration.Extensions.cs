using BookManagement.Application.Common.EventBus.Brokers;
using BookManagement.Application.Common.Settings;
using BookManagement.Application.Identity.Services;
using BookManagement.Domain.Common.Serializers;
using BookManagement.Domain.Constants;
using BookManagement.Infrastructure.Common.Caching;
using BookManagement.Infrastructure.Common.EventBus.Brokers;
using BookManagement.Infrastructure.Common.EventBus.Extensions;
using BookManagement.Infrastructure.Common.Serializers;
using BookManagement.Infrastructure.Identity.Services;
using BookManagement.Persistence.Caching.Brokers;
using BookManagement.Persistence.DataContexts;
using BookManagement.Persistence.Repositories.Interfaces;
using BookManagement.Persistence.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;
using BookManagement.Application.Books.Services;
using BookManagement.Infrastructure.Books.Services;
using BookManagement.Application.Verification.Services;
using BookManagement.Infrastructure.Verification.Services;
using BookManagement.Application.Notifications.Brokers;
using BookManagement.Application.Notifications.Services;
using BookManagement.Infrastructure.Notifications.Brokers;
using BookManagement.Infrastructure.Notifications.Services;
using MassTransit.SqlTransport;
using BookManagement.Domain.Brokers;
using BookManagement.Infrastructure.Common.RequestContexts.Brokers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using BookManagement.Api.Configurations;
using BookManagement.Api.Middlewares;
using BookManagement.Api.Data;

namespace BookManagement.Api.Configurations;

public static partial class HostConfiguration
{
    private static readonly ICollection<Assembly> Assemblies;

    static HostConfiguration()
    {
        Assemblies = Assembly.GetExecutingAssembly().GetReferencedAssemblies().Select(Assembly.Load).ToList();
        Assemblies.Add(Assembly.GetExecutingAssembly());
    }

    private static WebApplicationBuilder AddSerializers(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IJsonSerializationSettingsProvider, JsonSerializationSettingsProvider>();

        return builder;
    }

    private static WebApplicationBuilder AddMappers(this WebApplicationBuilder builder)
    {
        builder.Services.AddAutoMapper(Assemblies);

        return builder;
    }

    private static WebApplicationBuilder AddValidators(this WebApplicationBuilder builder)
    {
        // register configurations 
        builder.Services.Configure<ValidationSettings>(builder.Configuration.GetSection(nameof(ValidationSettings)));

        // register fluent validation
        builder.Services.AddValidatorsFromAssemblies(Assemblies).AddFluentValidationAutoValidation();

        return builder;
    }

    private static WebApplicationBuilder AddCaching(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<CacheSettings>(builder.Configuration.GetSection(nameof(CacheSettings)));

        builder.Services.AddLazyCache();

        builder.Services.AddSingleton<ICacheBroker, LazyMemoryCacheBroker>();

        builder.Services.AddSingleton<AccessTokenValidationMiddleware>();

        return builder;
    }

    private static WebApplicationBuilder AddEventBus(this WebApplicationBuilder builder)
    {
        builder
            .Services
            .AddMassTransit(configuration =>
            {
                var serviceProvider = configuration.BuildServiceProvider();
                var jsonSerializerSettingsProvider = serviceProvider.GetRequiredService<IJsonSerializationSettingsProvider>();

                configuration.RegisterAllConsumers(Assemblies);
                configuration.UsingInMemory((context, cfg) =>
                {
                    cfg.ConfigureEndpoints(context);

                    cfg.UseNewtonsoftJsonSerializer();
                    cfg.UseNewtonsoftJsonDeserializer();

                    cfg.ConfigureNewtonsoftJsonSerializer(settings => jsonSerializerSettingsProvider.ConfigureForEventBus(settings));
                    cfg.ConfigureNewtonsoftJsonDeserializer(settings => jsonSerializerSettingsProvider.ConfigureForEventBus(settings));
                });
            });

        builder.Services.AddSingleton<IEventBusBroker, MassTransitEventBusBroker>();

        return builder;
    }

    private static WebApplicationBuilder AddPersistence(this WebApplicationBuilder builder)
    {
        var dbConnectionString =
            builder.Configuration.GetConnectionString(DataAccessConstants.DbConnectionString) ??
            Environment.GetEnvironmentVariable(DataAccessConstants.DbConnectionString);

        var logger = builder.Services.BuildServiceProvider().GetService<ILogger<Program>>();

        logger?.LogInformation("Environment: {Environment}", builder.Environment.EnvironmentName);
        logger?.LogInformation("Connection String Present: {HasConnection}", !string.IsNullOrEmpty(dbConnectionString));
        logger?.LogDebug("Connection String: {ConnectionString}", dbConnectionString);

        builder.Services.AddDbContext<AppDbContext>(options => { options.UseNpgsql(dbConnectionString); });

        return builder;
    }

    private static WebApplicationBuilder AddIdentityInfrastructure(this WebApplicationBuilder builder)
    {
        // register configurations
        builder.Services.Configure<PasswordValidationSettings>(builder.Configuration.GetSection(nameof(PasswordValidationSettings)));
        builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(nameof(JwtSettings)));

        // register repositories
        builder.Services.AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IUserSettingsRepository, UserSettingsRepository>()
            .AddScoped<IAccessTokenRepository, AccessTokenRepository>();

        // register helper foundation services
        builder.Services.AddTransient<IPasswordHasherService, PasswordHasherService>()
            .AddTransient<IPasswordGeneratorService, PasswordGeneratorService>()
            .AddTransient<IAccessTokenGeneratorService, AccessTokenGeneratorService>();

        // register foundation data access services
        builder.Services.AddScoped<IUserService, UserService>()
            .AddScoped<IUserSettingsService, UserSettingsService>()
            .AddScoped<IAccessTokenService, AccessTokenService>();

        // register other higher services
        builder.Services.AddScoped<IAccountAggregatorService, AccountAggregatorService>()
            .AddScoped<IAuthAggregationService, AuthAggregationService>();

        // register authentication handlers
        var jwtSettings = builder.Configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>() ??
                          throw new InvalidOperationException("JwtSettings is not configured.");

        // add authentication
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(
                options =>
                {
                    options.RequireHttpsMetadata = false;

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = jwtSettings.ValidateIssuer,
                        ValidIssuer = jwtSettings.ValidIssuer,
                        ValidAudience = jwtSettings.ValidAudience,
                        ValidateAudience = jwtSettings.ValidateAudience,
                        ValidateLifetime = jwtSettings.ValidateLifetime,
                        ValidateIssuerSigningKey = jwtSettings.ValidateIssuerSigningKey,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
                    };
                }
            );

        return builder;
    }

    private static WebApplicationBuilder AddNotificationInfrastructure(this WebApplicationBuilder builder)
    {
        // register configurations 
        builder.Services.Configure<TemplateRenderingSettings>(builder.Configuration.GetSection(nameof(TemplateRenderingSettings)))
            .Configure<SmtpEmailSenderSettings>(builder.Configuration.GetSection(nameof(SmtpEmailSenderSettings)))
            //.Configure<TwilioSmsSenderSettings>(builder.Configuration.GetSection(nameof(TwilioSmsSenderSettings)))
            .Configure<NotificationSettings>(builder.Configuration.GetSection(nameof(NotificationSettings)));

        builder.Services.AddScoped<IEmailTemplateRepository, EmailTemplateRepository>().AddScoped<IEmailHistoryRepository, EmailHistoryRepository>();

        // register brokers
        builder.Services.AddScoped<IEmailSenderBroker, SmtpEmailSenderBroker>().AddScoped<IEmailHistoryRepository, EmailHistoryRepository>();

        // register data access foundation services
        _ = builder.Services.AddScoped<IEmailTemplateService, EmailTemplateService>().AddScoped<IEmailHistoryService, EmailHistoryService>();

        // register helper foundation services
        builder.Services.AddScoped<IEmailSenderService, EmailSenderService>().AddScoped<IEmailRenderingService, EmailRenderingService>();

        return builder;
    }

    private static WebApplicationBuilder AddVerificationInfrastructure(this WebApplicationBuilder builder)
    {
        // register configurations
        builder.Services.Configure<VerificationSettings>(builder.Configuration.GetSection(nameof(VerificationSettings)));

        // register repositories
        builder.Services.AddScoped<IUserInfoVerificationCodeRepository, UserInfoVerificationCodeRepository>();

        // register foundation data access services
        builder.Services.AddScoped<IUserInfoVerificationCodeService, UserInfoVerificationCodeService>();

        // register other higher services
        builder.Services.AddScoped<IVerificationProcessingService, VerificationProcessingService>();

        return builder;
    }

    private static WebApplicationBuilder AddBooksInfrastructure(this WebApplicationBuilder builder)
    {
        //registering repositories
        builder
            .Services
            .AddScoped<IBookRepository, BookRepository>();

        //registering services
        builder
            .Services
            .AddScoped<IBookService, BookService>();

        return builder;
    }

    private static WebApplicationBuilder AddMediatR(this WebApplicationBuilder builder)
    {
        builder
            .Services
            .AddMediatR(conf => { conf.RegisterServicesFromAssemblies(Assemblies.ToArray()); });

        return builder;
    }

    private static WebApplicationBuilder AddReqeuestContextTools(this WebApplicationBuilder builder)
    {
        builder.Services.AddHttpContextAccessor();

        builder.Services.AddScoped<IRequestContextProvider, RequestContextProvider>();

        return builder;
    }

    private static WebApplicationBuilder AddCors(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(
            options =>
            {
                options.AddDefaultPolicy(
                    policyBuilder =>
                    {
                        policyBuilder
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    }
                );
            }
        );

        return builder;
    }

    private static WebApplicationBuilder AddDevTools(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        return builder;
    }

    private static WebApplicationBuilder AddExposers(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<ApiBehaviorOptions>
            (options => { options.SuppressModelStateInvalidFilter = true; });

        builder.Services.AddRouting(options => options.LowercaseUrls = true);
        builder.Services.AddControllers().AddNewtonsoftJson();

        return builder;
    }

    private static async ValueTask<WebApplication> MigratedataBaseSchemasAsync(this WebApplication app)
    {
        var serviceScopeFactory = app.Services.GetRequiredKeyedService<IServiceScopeFactory>(null);

        await serviceScopeFactory.MigrateAsync<AppDbContext>();

        return app;
    }

    private static async ValueTask<WebApplication> SeedDataAsync(this WebApplication app)
    {
        var serviceScope = app.Services.CreateScope();
        await serviceScope.ServiceProvider.InitializeSeedAsync();

        return app;
    }

    private static WebApplication UseDevTools(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        return app;
    }

    private static WebApplication UseExposers(this WebApplication app)
    {
        app.MapControllers();

        return app;
    }

    private static WebApplication UseIdentityInfrustructure(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();

        return app;
    }
}