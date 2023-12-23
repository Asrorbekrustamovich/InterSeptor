using InterseptorSample.Models;
using Microsoft.EntityFrameworkCore;

namespace InterseptorSample.data
{
    public class Mydbcontext:DbContext
    {
        public Mydbcontext()
        {
            
        }
        public Mydbcontext(DbContextOptions<Mydbcontext>options):base(options)
        {
            
        }
        public DbSet<Cars>Cars { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
    }
}
