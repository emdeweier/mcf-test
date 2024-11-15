using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

[Table("ms_user")]
public class User
{
    [Key]
    public int user_id { get; set; }
    [MaxLength(20)]
    public string user_name { get; set; }
    [MaxLength(50)]
    public string password { get; set; }
    public bool is_active { get; set; }
}