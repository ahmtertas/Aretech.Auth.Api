namespace Aretech.Services.SeedWorks.DeviceInfoService
{
	public interface IDeviceInfoService
	{
		string GetOsName();
		(string? Ipv4Address, string? Ipv6Address) GetIpAddresses();
		string? GetMacAddress();
	}
}
