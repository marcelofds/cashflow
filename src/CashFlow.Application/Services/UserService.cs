using CashFlow.Application.DataTransferObjects;
using CashFlow.Domain.Aggregates;
using CashFlow.Domain.Aggregates.Repositories;
using CashFlow.Domain.Exceptions;

namespace CashFlow.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

    public User Get(UserDto user)
    {
        var logedUser = _repository.Get(user.UserName, user.Password);
        if (logedUser == null) throw new CashFlowNotFoundException("User not found, user ou password mismatch");

        return logedUser;
    }
}

public interface IUserService
{
    User Get(UserDto user);
}