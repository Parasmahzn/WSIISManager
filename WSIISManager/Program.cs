global using WSIISManager.Models;
using WSIISManager;
using WSIISManager.Services;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddTransient<IEmailService, EmailService>();
        services.AddTransient<IIISServiceExecution, IISServiceExecution>();
        services.AddHostedService<Worker>();
    })
    .UseWindowsService()
    .Build();

await host.RunAsync();
