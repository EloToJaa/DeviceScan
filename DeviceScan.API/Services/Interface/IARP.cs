using System.Net;
using System.Net.NetworkInformation;

namespace DeviceScan.API.Services.Interface
{
    public interface IARP
    {
        PhysicalAddress? GetMacAddress(IPAddress ipAddress);
    }
}