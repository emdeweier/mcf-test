namespace Client.Models;

public class Response<T>
{
    public int code { get; set; }
    public string message { get; set; }
    public T? data { get; set; }
}