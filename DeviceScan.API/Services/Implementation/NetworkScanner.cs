using DeviceScan.API.Data;
using DeviceScan.API.Models;
using DeviceScan.API.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace DeviceScan.API.Services.Implementation
{
    public class NetworkScanner
    {
        private readonly IDeviceService _deviceService;
        private readonly DataContext _dbContext;

        public NetworkScanner(DataContext dbContext, IDeviceService deviceService)
        {
            _dbContext = dbContext;
            _deviceService = deviceService;
        }

        public async Task ScanNetworkAsync()
        {
            var devices = await _deviceService.GetDevicesAsync("192.168.0");
            foreach (var device in devices)
            {
                var existingDevice = await _dbContext.Devices.FirstOrDefaultAsync(d => d.IpAddress == device.IpAddress!.ToString());
                if (existingDevice != null)
                {
                    existingDevice.MacAddress = device.MacAddress!.ToString();
                    existingDevice.LastSeen = DateTime.UtcNow;
                }
                else
                {
                    var newDevice = new Scan
                    {
                        IpAddress = device.IpAddress!.ToString(),
                        MacAddress = device.MacAddress!.ToString(),
                        LastSeen = DateTime.UtcNow
                    };

                    _dbContext.Devices.Add(newDevice);
                }
            }

            _dbContext.SaveChanges();
        }
    }
}
