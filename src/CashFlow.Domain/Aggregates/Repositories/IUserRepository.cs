namespace CashFlow.Domain.Aggregates.Repositories;

public interface IUserRepository
{
    User? Get(string username, string password);
}