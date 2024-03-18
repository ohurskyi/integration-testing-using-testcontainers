using System.Net.Mime;
using Devices.Api.DataAccess;
using Devices.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Devices.Api.Controllers;

[Produces(MediaTypeNames.Application.Json)]
[ApiController]
[Route("api/[controller]")]
public class DevicesController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;

    public DevicesController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<List<DeviceDto>> Get(CancellationToken cancellationToken)
    {
        var conn = _dbContext.Database.GetConnectionString();
        var devices = await _dbContext.Devices
            .Select(x => new DeviceDto
            {
                Id = x.Id,
                Name = x.Name,
                MacAddress = x.MacAddress
            })
            .OrderBy(x => x.Name)
            .Take(100)
            .ToListAsync(cancellationToken: cancellationToken);
        return devices;
    }
}