using API.Context;
using API.Models;
using API.Repositories.Interfaces;

namespace API.Repositories;

public class LocationRepository : ILocationRepository
{
    private MyContext _context;
    public LocationRepository(MyContext context)
    {
        _context = context;
    }

    public List<Location>? GetLocation()
    {
        try
        {
            List<Location>? locations = _context.Locations.ToList();
            return locations;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Location? GetLocationById(string location_id)
    {
        try
        {
            Location? locations = _context.Locations.Where(location =>
                location.location_id.Equals(location_id)).FirstOrDefault();
            return locations;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}