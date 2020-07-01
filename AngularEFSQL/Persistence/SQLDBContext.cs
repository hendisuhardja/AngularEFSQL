using AngularEFSQL.Data;
using Microsoft.EntityFrameworkCore;

namespace AngularEFSQL.Persistence
{
    public class SQLDBContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Invoice> Invoices { get; set; }

        public SQLDBContext()
        {

        }
        public SQLDBContext(DbContextOptions<SQLDBContext> options) : base(options)
        {
        }

    }
}
