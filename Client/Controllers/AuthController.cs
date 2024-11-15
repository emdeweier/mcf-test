using System.Net.Http.Headers;
using Client.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Client.Controllers;

public class AuthController : Controller
{
    readonly HttpClient _httpClient = new HttpClient();

    public AuthController()
    {
        _httpClient.BaseAddress = new Uri("https://localhost:7154/api/");
    }

    public IActionResult Index()
    {
        var username = HttpContext.Session.GetString("username");
        if (username != null)
        {
            return RedirectToAction("Index", "Home");
        }
        return View();
    }

    [HttpPost]
    public async Task<ActionResult> Login(User user)
    {
        var myContent = JsonConvert.SerializeObject(user);
        var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
        var byteContent = new ByteArrayContent(buffer);
        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        var affectedRow = _httpClient.PostAsync("User/Login", byteContent).Result;
        if (affectedRow.IsSuccessStatusCode)
        {
            var stringResponse = affectedRow.Content.ReadAsStringAsync().Result;
            var response = JsonConvert.DeserializeObject<Response<User>>(stringResponse);
            if (response?.data != null)
            {
                HttpContext.Session.SetString("username", response.data.user_name);
                return Json(new { statusCode = 200 });
            }
            else
            {
                return Json(new { statusCode = 500 });
            }
        }

        return Json(new { statusCode = 200 });
    }

    [HttpGet]
    public ActionResult Logout()
    {
        var username = HttpContext.Session.GetString("username");
        if (username == null)
        {
            return RedirectToAction("", "Auth");
        }

        HttpContext.Session.Remove("username");
        return RedirectToAction(nameof(Index));
    }
}