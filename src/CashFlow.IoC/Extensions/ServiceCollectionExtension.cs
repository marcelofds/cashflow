using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using CashFlow.Application.Services;
using CashFlow.Data.Context;
using CashFlow.Data.Repositories;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.IoC.Extensions;

public static class ServiceCollectionExtension
{
    private static IServiceCollection _services = null!;
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        _services = services;
        ConfigureContext(configuration);
        ConfigureTools(configuration);
        RegisterApplicationServices(configuration);
        RegisterRepositories(configuration);
    }

    private static void RegisterApplicationServices(IConfiguration configuration)
    {
        _services.Scan(scan => scan.FromAssemblyOf<BillingService>()
            .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Service")))
            .AsMatchingInterface()
            .WithScopedLifetime()
        );
    }

    private static void RegisterRepositories(IConfiguration configuration)
    {
        _services.Scan(scan => scan.FromAssemblyOf<BillsToPayRepository>()
            .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Repository")))
            .AsMatchingInterface()
            .WithScopedLifetime()
        );
    }

    private static void ConfigureTools(IConfiguration configuration)
    {
        _services.AddSingleton(TypeAdapterConfig.GlobalSettings);
        _services.AddScoped<IMapper, ServiceMapper>();
    }

    private static void ConfigureContext(IConfiguration configuration)
    {
        var migrationsAssembly = typeof(CashFlowContext).GetTypeInfo().Assembly.GetName().Name;
        var connectionString = configuration["ConnectionStr"];
        //Connection injected to be used into the repositories.
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        _services.AddEntityFrameworkNpgsql()
            .AddDbContext<CashFlowContext>(builder =>
            {
                builder.UseNpgsql(connectionString,
                    options =>
                    {
                        options.MigrationsAssembly(migrationsAssembly);
                        options.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30),
                            errorCodesToAdd: null);
                    });
            });
    }
}