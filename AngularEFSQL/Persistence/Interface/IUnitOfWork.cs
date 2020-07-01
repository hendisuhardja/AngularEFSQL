using System.Threading.Tasks;

namespace AngularEFSQL.Persistence.Interface
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}
