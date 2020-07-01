using AngularEFSQL.Data;
using AngularEFSQL.Dto;
using AngularEFSQL.Extensions;
using AngularEFSQL.Persistence.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AngularEFSQL.Persistence
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly SQLDBContext context;
        public CustomerRepository(SQLDBContext context)
        {
            this.context = context;

        }
        public async Task<Customer> GetCustomer(int id, bool includeRelated = true)
        {

            if (!includeRelated)
                return await context.Customers.FindAsync(id);

            var customer = await context.Customers
                .Include(x => x.Invoices)
                .SingleOrDefaultAsync(x => x.CustomerId == id);

            return customer;
        }

        public async Task Add(Customer customer)
        {
            await context.Customers.AddAsync(customer);
        }

        public void Remove(Customer customer)
        {
            context.Customers.Remove(customer);
        }

        public async Task<QueryResultDto<Customer>> GetCustomers(CustomerQuery queryObj)
        {
            var result = new QueryResultDto<Customer>();
            var query = context.Customers
                .Include(x => x.Invoices)
                .AsQueryable();

            query = ApplyFiltering(queryObj, query);

            var columnsMap = new Dictionary<string, Expression<Func<Customer, object>>>
            {
                ["address"] = v => v.Address,
                ["customerId"] = v => v.CustomerId,
                ["firstName"] = v => v.FirstName,
                ["lastName"] = v => v.LastName
            };

            query = query.ApplyOrdering(queryObj, columnsMap);

            result.TotalItems = await query.CountAsync();

            query = query.ApplyPaging(queryObj);

            result.Items = await query.ToListAsync();

            return result;
        }


        private IQueryable<Customer> ApplyFiltering(CustomerQuery queryObj, IQueryable<Customer> query)
        {
            if (!string.IsNullOrEmpty(queryObj.Address))
                query = query.Where(v => v.Address.Trim().ToLower() == queryObj.Address.Trim().ToLower());

            if (queryObj.CustomerId.HasValue)
                query = query.Where(v => v.CustomerId == queryObj.CustomerId.Value);

            if (!string.IsNullOrEmpty(queryObj.FirstName))
                query = query.Where(v => v.FirstName.Trim().ToLower() == queryObj.FirstName.Trim().ToLower());

            if (!string.IsNullOrEmpty(queryObj.LastName))
                query = query.Where(v => v.LastName.Trim().ToLower() == queryObj.LastName.Trim().ToLower());

            return query;
        }
    }
}
