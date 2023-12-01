using System.Net;
using System.Net.NetworkInformation;

namespace DeviceScan.API.Models
{
    public class Device
    {
        public IPAddress? IpAddress { get; set; }
        public PhysicalAddress? MacAddress { get; set; }
    }
}
