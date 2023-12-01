using DeviceScan.API.Models;
using System.Net.NetworkInformation;

namespace DeviceScan.API.Services.Interface
{
    public interface IDeviceService
    {
        Task<Device?> CheckDeviceAsync(string ipAddress);
        Task<List<Device>> FindDevicesByMAC(string[] macs, string baseIpAddress);
        Task<List<Device>> GetDevicesAsync(string baseIpAddress);
        PhysicalAddress? GetMacAddress(string ipAddress);
    }
}