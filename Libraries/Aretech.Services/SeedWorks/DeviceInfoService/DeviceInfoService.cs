using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace Aretech.Services.SeedWorks.DeviceInfoService
{
	public class DeviceInfoService : IDeviceInfoService
	{
		public string GetOsName()
		{
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
				return "Windows";
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
				return "Linux";
			if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
				return "macOS";

			return "Unknown OS";
		}

		public (string? Ipv4Address, string? Ipv6Address) GetIpAddresses()
		{
			string? ipv4 = null;
			string? ipv6 = null;

			var host = Dns.GetHostEntry(Dns.GetHostName());
			foreach (var ip in host.AddressList)
			{
				if (ip.AddressFamily == AddressFamily.InterNetwork)
					ipv4 = ip.ToString();
				else if (ip.AddressFamily == AddressFamily.InterNetworkV6)
					ipv6 = ip.ToString();
			}

			return (ipv4, ipv6);
		}


		public string? GetMacAddress()
		{
			var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
			foreach (var ni in networkInterfaces)
			{
				if (ni.OperationalStatus == OperationalStatus.Up && ni.NetworkInterfaceType != NetworkInterfaceType.Loopback)
				{
					var macAddress = ni.GetPhysicalAddress();
					return string.Join(":", macAddress.GetAddressBytes().Select(b => b.ToString("X2")));
				}
			}
			return null;
		}
	}
}
