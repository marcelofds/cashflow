using CashFlow.Domain.Aggregates;
using CashFlow.Domain.Aggregates.Repositories;

namespace CashFlow.Data.Repositories;

public class UserRepository : IUserRepository
{
    public User? Get(string username, string password)
    {
        var users = new List<User>
        {
            new() {Id = 1, UserName = "cashusr1", Password = "123@", Role = "Manager"},
            new() {Id = 2, UserName = "cashusr2", Password = "456@", Role = "Employee"}
        };
        return users.FirstOrDefault(u => u.UserName == username && u.Password == password);
    }
}