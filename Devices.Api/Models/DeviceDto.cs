namespace Devices.Api.Models;

public class DeviceDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string MacAddress { get; set; }
}