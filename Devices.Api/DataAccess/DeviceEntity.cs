namespace Devices.Api.DataAccess;

public class DeviceEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string MacAddress { get; set; }
}