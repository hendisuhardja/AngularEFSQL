using AngularEFSQL.Data;
using AngularEFSQL.Dto;
using System.Threading.Tasks;

namespace AngularEFSQL.Persistence.Interface
{
    public interface ICustomerRepository
    {

        Task Add(Customer customer);
        Task<Customer> GetCustomer(int id, bool includeRelated = true);
        void Remove(Customer customer);

        Task<QueryResultDto<Customer>> GetCustomers(CustomerQuery filter);
    }
}
