using AngularEFSQL.Extensions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AngularEFSQL.Data
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public virtual List<Invoice> Invoices { get; set; }
    }

    public class CustomerQuery : IQueryObject
    {

        public int? CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }

        public string SortBy { get; set; }

        public bool IsSortAscending { get; set; }

        public int Page { get; set; }

        public int PageSize { get; set; }
    }
}
