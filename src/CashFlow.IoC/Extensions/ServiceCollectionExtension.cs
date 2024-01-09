using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Text;
using CashFlow.Application.Services;
using CashFlow.Application.Utils;
using CashFlow.Data.Context;
using CashFlow.Data.Repositories;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

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
        ConfigureAuthentication(services);
    }

    private static void ConfigureAuthentication(IServiceCollection services)
    {
        var secretKey = Encoding.ASCII.GetBytes(Settings.Secret);
        services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
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

    public static void AddSwaggerDocumentations(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "APIContagem", Version = "v1" });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description =
                    "JWT Authorization Header - utilizado com Bearer Authentication.\r\n\r\n" +
                    "Digite 'Bearer' [espaço] e então seu token no campo abaixo.\r\n\r\n" +
                    "Exemplo (informar sem as aspas): 'Bearer 12345abcdef'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
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