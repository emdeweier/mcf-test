using API.Context;
using API.Models;
using API.Repositories.Interfaces;

namespace API.Repositories;

public class UserRepository : IUserRepository
{
    private MyContext _context;
    public UserRepository(MyContext context)
    {
        _context = context;
    }
    
    public List<User>? GetUser()
    {
        try
        {
            List<User>? users = _context.Users.ToList();
            return users;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public User? GetUserByUsername(string username)
    {
        try
        {
            User? users = _context.Users.Where(user =>
            user.user_name.Equals(username)).FirstOrDefault();
            return users;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}