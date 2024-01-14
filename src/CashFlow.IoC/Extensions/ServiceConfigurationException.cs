using CashFlow.Data.Context;
using CashFlow.IoC.Factories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.IoC.Extensions;

public static class ServiceConfigurationExtension
{
    private static IApplicationBuilder _applicationBuilder = null!;

    public static void ConfigureApplication(this IApplicationBuilder app)
    {
        _applicationBuilder = app ?? throw new ArgumentNullException();
        RunMigration();
    }

    public static void ConfigureAccessor(this IServiceCollection services)
    {
        var sp = services.BuildServiceProvider();
        var accessor = sp.GetRequiredService<IHttpContextAccessor>();
        ContainerAccessorUtil.Instance.ContainerAccessor = () => accessor.HttpContext?.RequestServices!;
    }

    private static void RunMigration()
    {
        using var serviceScope =
            _applicationBuilder.ApplicationServices.GetService<IServiceScopeFactory>()?.CreateScope();
        var contextConfig = serviceScope?.ServiceProvider.GetRequiredService<CashFlowContext>();
        contextConfig?.Database.Migrate();
    }
}