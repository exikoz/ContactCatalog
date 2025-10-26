using ContactCatalog.Services;
using ContactCatalog.UI;
using Microsoft.Extensions.Logging;

namespace ContactCatalog;

public class Application
{
    private readonly ContactService _service;
    private readonly ConsoleMenu _menu;
    private readonly ILogger<Application> _logger;

    public Application()
    {
        // Setup logging
        using var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
            builder.SetMinimumLevel(LogLevel.Information);
        });

        _logger = loggerFactory.CreateLogger<Application>();
        var repositoryLogger = loggerFactory.CreateLogger<ContactRepository>();
        var serviceLogger = loggerFactory.CreateLogger<ContactService>();

        // Create dependencies with logging
        var repository = new ContactRepository(repositoryLogger);
        _service = new ContactService(repository, serviceLogger);
        _menu = new ConsoleMenu(_service);

        _logger.LogInformation("Application started");
    }

    public void Run()
    {
        _menu.Run();
        _logger.LogInformation("Application stopped");
    }
}