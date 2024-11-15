namespace Client.Models;

public class User
{
    public int user_id { get; set; }
    public string user_name { get; set; }
    public string password { get; set; }
    public bool is_active { get; set; }
}