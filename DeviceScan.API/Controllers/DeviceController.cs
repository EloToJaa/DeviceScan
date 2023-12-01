using DeviceScan.API.DTOs;
using DeviceScan.API.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace DeviceScan.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly IDeviceService _deviceService;

        public DeviceController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var devices = await _deviceService.GetDevicesAsync("192.168.0");
            var dto = devices.Select(d => new DeviceDto
            {
                IpAddress = d.IpAddress!.ToString(),
                MacAddress = d.MacAddress is null ? string.Empty : d.MacAddress.ToString(),
            });

            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Find([FromBody]string[] macs)
        {
            var devices = await _deviceService.FindDevicesByMAC(macs, "192.168.0");
            var dto = devices.Select(d => new DeviceDto
            {
                IpAddress = d.IpAddress!.ToString(),
                MacAddress = d.MacAddress is null ? string.Empty : d.MacAddress.ToString(),
            });

            return Ok(dto);
        }
    }
}
