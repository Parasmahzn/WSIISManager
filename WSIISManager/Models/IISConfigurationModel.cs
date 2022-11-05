namespace WSIISManager.Models;

public class IISConfigModel
{
    public Iisconfiguration IISConfiguration { get; set; } = new();
}

public class Iisconfiguration
{
    public Email Email { get; set; } = new();
    public List<string> ApplicationPool { get; set; } = new();
    public List<string> Sites { get; set; } = new();
}

public class Email
{
    public Source Source { get; set; } = new();
    public string Destination { get; set; } = "";
}

public class Source
{
    public string Username { get; set; } = "";
    public string Password { get; set; } = "";
    public string Host { get; set; } = "smtp.gmail.com";
    public int Port { get; set; } = 587;
}
