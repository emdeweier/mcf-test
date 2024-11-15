using API.Models;
using API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private IUserRepository _userRepository;
    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    [HttpPost("GetUsers")]
    public async Task<ActionResult> GetUser()
    {
        Response<List<User>> response = new Response<List<User>>()
        {
            code = 200,
            message = "Success",
            data = null
        };
        try
        {
            List<User>? users = _userRepository.GetUser();
            response.data = users;
            return Ok(response);
        }
        catch (Exception e)
        {
            response = new Response<List<User>>()
            {
                code = 500,
                message = "Internal Server Error",
                data = null
            };
            Console.WriteLine(e);
            return Ok(response);
        }
    }
    
    [HttpPost]
    public async Task<ActionResult> GetUserByUsername(string username)
    {
        Response<User> response = new Response<User>()
        {
            code = 200,
            message = "Success",
            data = null
        };
        try
        {
            User? users = _userRepository.GetUserByUsername(username);
            if (users != null)
            {
                response.data = users;
            }
            else
            {
                response = new Response<User>()
                {
                    code = 200,
                    message = "User Not Found",
                    data = null
                };
            }
            return Ok(response);
        }
        catch (Exception e)
        {
            response = new Response<User>()
            {
                code = 500,
                message = "Internal Server Error",
                data = null
            };
            Console.WriteLine(e);
            return Ok(response);
        }
    }

    [HttpPost("Login")]
    public async Task<ActionResult> Login(User user)
    {
        Response<User> response = new Response<User>()
        {
            code = 200,
            message = "Success",
            data = null
        };
        if (ModelState.IsValid)
        {
            try
            {
                var userResult = _userRepository.GetUserByUsername(user.user_name);
                if (userResult != null)
                {
                    var isPasswordMatch = user.password.Equals(userResult.password);
                    if (isPasswordMatch)
                    {
                        if (!userResult.is_active)
                        {
                            response = new Response<User>()
                            {
                                code = 500,
                                message = "User is Not Active",
                                data = null
                            };
                        }
                        response.data = userResult;
                        return Ok(response);
                    }

                    response = new Response<User>()
                    {
                        code = 500,
                        message = "Incorrect Password",
                        data = null
                    };
                    return Ok(response);
                }
                
                return Ok(response);
            }
            catch (Exception e)
            {
                response = new Response<User>()
                {
                    code = 500,
                    message = "Internal Server Error",
                    data = null
                };
                Console.WriteLine(e);
                return Ok(response);
            }
        }

        return BadRequest();
    }
}