using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DeviceScan.API.Models
{
    [Index(nameof(IpAddress), IsUnique = true)]
    [Index(nameof(MacAddress), IsUnique = true)]
    public class Scan
    {
        [Key]
        public int Id { get; set; }

        public string IpAddress { get; set; } = string.Empty;
        public string MacAddress { get; set; } = string.Empty;
        public DateTime LastSeen { get; set; }
    }
}
