using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.IoC.Factories;

public class ContainerAccessorUtil
{
    public static ContainerAccessorUtil Instance { get; } = new();

    public Func<IServiceProvider> ContainerAccessor { get; set; } = null!;

    public IServiceProvider Container => ContainerAccessor();
}

public class ContainerFactory
{
    public static T? GetInstance<T>()
    {
        return ContainerAccessorUtil.Instance.Container.GetService<T>();
    }
}