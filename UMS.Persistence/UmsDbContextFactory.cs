using Microsoft.EntityFrameworkCore;
using UMS.Persistence.Models;

namespace UMS.Persistence
{
    public class PcpDbContextFactory 
        : DesignTimeDbContextFactoryBase<UmsContext>
    {
        protected override UmsContext CreateNewInstance(DbContextOptions<UmsContext> options)
        {
            return new UmsContext(options);
        }
    }
}