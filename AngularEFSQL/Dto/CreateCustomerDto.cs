using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AngularEFSQL.Dto
{
    public class SaveCustomerDto
    {
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required]
        public string Address { get; set; }
        public ICollection<CreateInvoiceDto> Invoices { get; set; }
    }
}
