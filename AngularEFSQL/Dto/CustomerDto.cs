using System.Collections.Generic;

namespace AngularEFSQL.Dto
{
    public class CustomerDto
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public List<InvoiceDto> Invoices { get; set; }
    }
}
