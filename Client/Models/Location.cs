using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Client.Models;

public class Location
{
    public string location_id { get; set; }
    public string location_name { get; set; }
    
    public virtual ICollection<Transaction>? transactions { get; set; }
}