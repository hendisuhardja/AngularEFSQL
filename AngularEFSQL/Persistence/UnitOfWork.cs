using AngularEFSQL.Persistence.Interface;
using System.Threading.Tasks;

namespace AngularEFSQL.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SQLDBContext context;
        public UnitOfWork(SQLDBContext context)
        {
            this.context = context;

        }
        public async Task CompleteAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
