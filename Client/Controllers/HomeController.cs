using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Client.Models;
using Newtonsoft.Json;

namespace Client.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    readonly HttpClient _httpClient = new HttpClient();

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
        _httpClient.BaseAddress = new Uri("https://localhost:7154/api/");
    }

    public IActionResult Index()
    {
        if (HttpContext.Session.GetString("username") == null)
        {
            return RedirectToAction("Index", "Auth");    
        }
        
        ViewBag.Transactions = Transactions();
        ViewBag.Locations = Locations();
        
        return View();
    }
    
    public IList<Location> Locations()
    {
        IList<Location> locations = null;
        var responseTask = _httpClient.PostAsync("Location/GetLocations", null);
        var result = responseTask.Result;
        if (result.IsSuccessStatusCode)
        {
            var readTask = result.Content.ReadAsStringAsync().Result;
            var resultLocation = JsonConvert.DeserializeObject<Response<List<Location>>>(readTask);
            if (resultLocation?.data != null)
            {
                locations = resultLocation.data;
            }
            // questions = resultLocation;
        }
        return locations;
    }
    
    public IList<Transaction> Transactions()
    {
        IList<Transaction> transactions = null;
        var responseTask = _httpClient.PostAsync("Transaction/GetTransactions", null);
        var result = responseTask.Result;
        if (result.IsSuccessStatusCode)
        {
            var readTask = result.Content.ReadAsStringAsync().Result;
            var resultTransaction = JsonConvert.DeserializeObject<Response<List<Transaction>>>(readTask);
            if (resultTransaction?.data != null)
            {
                transactions = resultTransaction.data;
            }
            // questions = resultLocation;
        }
        return transactions;
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}