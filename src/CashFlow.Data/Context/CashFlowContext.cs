using System.Reflection;
using CashFlow.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Data.Context;

public interface IPlatformContext : IContextBase
{
}

public class CashFlowContext : ContextBase, IPlatformContext
{
    public CashFlowContext(DbContextOptions<CashFlowContext> options /*, ILoggedInUser loggedInUser */) : base(options)
    {
        //_loggedInUser = loggedInUser;
    }

    //Entities
    public DbSet<BillToReceive> BillsToReceives { get; set; }
    public DbSet<BillToPay> BillsToPays { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Customer> Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<BillToPay>().ToTable(nameof(BillToPay));
        builder.Entity<BillToReceive>().ToTable(nameof(BillToReceive));
        builder.Entity<Supplier>().ToTable(nameof(Supplier));
        builder.Entity<Customer>().ToTable(nameof(Customer));

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);

        foreach (var entity in builder.Model.GetEntityTypes())
        {
            // Replace the table names
            entity.SetTableName(entity.GetTableName()?.ToLower());
            // Replace the column names
            foreach (var property in entity.GetProperties())
                property.SetColumnName(property.Name.ToLower());
            foreach (var key in entity.GetKeys())
                key.SetName(key.GetName()?.ToLower());
            foreach (var key in entity.GetForeignKeys())
                key.SetConstraintName(key.GetConstraintName()?.ToLower());
        }
    }

    #region Private

    //private readonly ILoggedInUser _loggedInUser;

    #endregion
}