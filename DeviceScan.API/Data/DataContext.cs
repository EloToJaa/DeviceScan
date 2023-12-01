using DeviceScan.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DeviceScan.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Scan> Devices { get; set; }
    }
}
