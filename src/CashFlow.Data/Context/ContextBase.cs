
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using CashFlow.Data.Extensions;

namespace CashFlow.Data.Context;
public interface IContextBase : IDisposable
{
    int SaveChanges();
    Task SaveChangesAsync();
}
public abstract class ContextBase : DbContext, IContextBase
{
    public ContextBase(DbContextOptions options) : base(options)
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        MethodInfo addMethod = typeof(ModelBuilderExtensions)
            .GetMethods()
            .Single(m =>
                m.Name == "AddConfiguration"
                && m.GetGenericArguments().Any(a => a.Name == "TEntity"));

        foreach (var assembly in AppDomain.CurrentDomain
                     .GetAssemblies()
                     .Where(a => a.GetName().Name != "EntityFramework"))
        {
            var configTypes = assembly
                .GetTypes()
                .Where(t => t.BaseType != null
                            && t.BaseType.IsGenericType
                            && t.BaseType.GetGenericTypeDefinition() == typeof(ModelBuilderExtensions.EntityTypeConfiguration<>));

            foreach (var type in configTypes)
            {
                var entityType = type.BaseType?.GetGenericArguments().Single();

                var entityConfig = assembly.CreateInstance(type.FullName!);
                addMethod.MakeGenericMethod(entityType!)
                    .Invoke(builder, new[] {builder, entityConfig!});
            }
        }
    }

    public virtual async Task SaveChangesAsync()
    {
        await base.SaveChangesAsync();
    }
    
}