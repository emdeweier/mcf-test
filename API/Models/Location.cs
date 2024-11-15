using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Models;

[Table(("ms_storage_location"))]
public class Location
{
    [Key]
    [MaxLength(10)]
    public string location_id { get; set; }
    [MaxLength(100)]
    public string location_name { get; set; }
    
    [JsonIgnore]
    public virtual ICollection<Transaction>? Transactions { get; set; }
}