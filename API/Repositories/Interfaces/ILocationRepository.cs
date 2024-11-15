using API.Models;

namespace API.Repositories.Interfaces;

public interface ILocationRepository
{
    List<Location>? GetLocation();
    Location? GetLocationById(string location_id);
}