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
    public string Destination { get; set; } = string.Empty;
}

public class Source
{
    public string username { get; set; } = string.Empty;
    public string password { get; set; } = string.Empty;
}
