using Devices.Api.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Devices.Api.Services;

public class MigrateDbBackgroundService : IHostedService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<MigrateDbBackgroundService> _logger;

    public MigrateDbBackgroundService(IServiceScopeFactory serviceScopeFactory, ILogger<MigrateDbBackgroundService> logger)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Applying db migrations ...");

            await MigrateDatabase(cancellationToken);

            _logger.LogInformation("Migrations successfully applied");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while applying db migrations");
            throw;
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private async Task MigrateDatabase(CancellationToken cancellationToken)
    {
        using var serviceScope = _serviceScopeFactory.CreateScope();
        var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await dbContext.Database.MigrateAsync(cancellationToken);
    }
}