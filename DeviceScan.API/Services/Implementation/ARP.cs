using System.Net.NetworkInformation;
using System.Net;
using System.Runtime.InteropServices;
using DeviceScan.API.Services.Interface;

namespace DeviceScan.API.Services.Implementation
{
    public class ARP : IARP
    {
        public PhysicalAddress? GetMacAddress(IPAddress ipAddress)
        {
            try
            {
                byte[] macAddr = new byte[6];
                int macAddrLen = macAddr.Length;

                uint destIP = BitConverter.ToUInt32(ipAddress.GetAddressBytes(), 0);
                uint srcIP = BitConverter.ToUInt32(IPAddress.Any.GetAddressBytes(), 0);

                int ret = SendARP(destIP, srcIP, macAddr, ref macAddrLen);
                if (ret == 0)
                {
                    return new PhysicalAddress(macAddr);
                }
                else
                {
                    Console.WriteLine($"Error retrieving MAC address. Error code: {ret}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving MAC address: {ex.Message}");
                return null;
            }
        }

        [DllImport("iphlpapi.dll", ExactSpelling = true)]
        public static extern int SendARP(uint destIP, uint srcIP, byte[] macAddr, ref int macAddrLen);
    }
}
