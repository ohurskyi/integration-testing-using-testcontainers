using Bogus;
using Devices.Api.DataAccess;
using Devices.Api.Models;

namespace Api.Tests.Integration;

public class DevicesTest : BaseIntegrationTest
{
    private readonly Faker<DeviceEntity> faker=  new Faker<DeviceEntity>()
        .RuleFor(x => x.Name, x => x.Lorem.Word())
        .RuleFor(x => x.MacAddress, x => x.Internet.Mac());
    
    public DevicesTest(DevicesWebApiFactory webApiFactory) : base(webApiFactory)
    {
    }

    [Fact]
    public async Task GetDevices_ReturnAllDevices_WhenDevicesExist()
    {
        // Arrange
        using var scope = ServiceScopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var device = faker.Generate();
        await dbContext.AddAsync(device);
        await dbContext.SaveChangesAsync();

        // Act
        var response = await HttpClient.GetAsync("api/devices");
        var actualDevices = await response.Content.ReadFromJsonAsync<List<DeviceDto>>();

        // Assert
        Assert.True(response.IsSuccessStatusCode);
        var actualDevice = actualDevices[0];
        Assert.Equal(actualDevice.Name, actualDevice.Name);
        Assert.Equal(device.MacAddress, actualDevice.MacAddress);
    }
}