using API.Models;

namespace API.Repositories.Interfaces;

public interface IUserRepository
{
    List<User>? GetUser();
    User? GetUserByUsername(string username);
}