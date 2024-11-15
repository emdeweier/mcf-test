using API.Models;
using API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LocationController : ControllerBase
{
    private ILocationRepository _locationRepository;
    public LocationController(ILocationRepository locationRepository)
    {
        _locationRepository = locationRepository;
    }
    
    [HttpPost("GetLocations")]
    public async Task<ActionResult> GetLocations()
    {
        Response<List<Location>> response = new Response<List<Location>>()
        {
            code = 200,
            message = "Success",
            data = null
        };
        try
        {
            List<Location>? locations = _locationRepository.GetLocation();
            response.data = locations;
            return Ok(response);
        }
        catch (Exception e)
        {
            response = new Response<List<Location>>()
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
    public async Task<ActionResult> GetLocationById(string location_id)
    {
        Response<Location> response = new Response<Location>()
        {
            code = 200,
            message = "Success",
            data = null
        };
        try
        {
            Location? location = _locationRepository.GetLocationById(location_id);
            if (location != null)
            {
                response.data = location;
            }
            else
            {
                response = new Response<Location>()
                {
                    code = 200,
                    message = "Location Not Found",
                    data = null
                };
            }
            return Ok(response);
        }
        catch (Exception e)
        {
            response = new Response<Location>()
            {
                code = 500,
                message = "Internal Server Error",
                data = null
            };
            Console.WriteLine(e);
            return Ok(response);
        }
    }
}