using System.Net.Http.Headers;
using Client.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Client.Controllers;

public class TransactionController : Controller
{
    readonly HttpClient _httpClient = new HttpClient();

    public TransactionController()
    {
        _httpClient.BaseAddress = new Uri("https://localhost:7154/api/");
    }
    
    public async Task<ActionResult> CreateTransaction(Transaction transaction)
    {
        if (HttpContext.Session.GetString("username") == null)
        {
            return RedirectToAction("Index", "Auth");    
        }
        
        transaction.created_by = HttpContext.Session.GetString("username");
        transaction.created_on  = DateTime.Now;
        transaction.last_updated_by = HttpContext.Session.GetString("username");
        transaction.last_updated_on  = DateTime.Now;
        var myContent = JsonConvert.SerializeObject(transaction);
        var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
        var byteContent = new ByteArrayContent(buffer);
        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        var affectedRow = _httpClient.PostAsync("Transaction/CreateTransaction", byteContent).Result;
        return Json(new { data = affectedRow });
    }
}