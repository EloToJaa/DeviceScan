using System.Net.NetworkInformation;
using System.Net;
using DeviceScan.API.Services.Interface;
using DeviceScan.API.Models;

namespace DeviceScan.API.Services.Implementation
{
    public class DeviceService : IDeviceService
    {
        private readonly IARP _arp = new ARP();

        public async Task<List<Device>> FindDevicesByMAC(string[] macs, string baseIpAddress)
        {
            var devices = await GetDevicesAsync(baseIpAddress);

            return devices
                .Where(d => macs.Contains(d.MacAddress!.ToString()))
                .ToList();
        }

        public async Task<List<Device>> GetDevicesAsync(string baseIpAddress)
        {
            var tasks = new List<Task<Device?>>();

            for (int i = 0; i < 256; i++)
            {
                string ipAddress = $"{baseIpAddress}.{i}";
                tasks.Add(CheckDeviceAsync(ipAddress));
            }

            var devices = await Task.WhenAll(tasks);

            return devices.Where(d => d != null).Select(d => d!).ToList();
        }

        public async Task<Device?> CheckDeviceAsync(string ipAddress)
        {
            var ping = new Ping();
            var reply = await ping.SendPingAsync(ipAddress, 1000);
            var ip = IPAddress.Parse(ipAddress);

            if (reply.Status == IPStatus.Success)
            {
                var macAddress = GetMacAddress(ipAddress);
                if (macAddress != null) return new Device
                {
                    IpAddress = ip,
                    MacAddress = macAddress
                };
                else return new Device
                {
                    IpAddress = ip,
                    MacAddress = null
                };
            }
            return null;
        }

        public PhysicalAddress? GetMacAddress(string ipAddress)
        {
            try
            {
                return _arp.GetMacAddress(IPAddress.Parse(ipAddress));
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
