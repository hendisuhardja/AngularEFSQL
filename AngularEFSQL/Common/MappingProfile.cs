using AngularEFSQL.Data;
using AngularEFSQL.Dto;
using AutoMapper;
using System.Linq;

namespace AngularEFSQL.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap(typeof(QueryResultDto<>), typeof(QueryResultDto<>));
            CreateMap<CustomerQueryDto, CustomerQuery>();

            CreateMap<Customer, CustomerDto>()
                .ForMember(vr => vr.Invoices, opt => opt.MapFrom(v => v.Invoices.Select(vf => new InvoiceDto { Id =  vf.Id, Date = vf.Date })));
            
            CreateMap<SaveCustomerDto, Customer>()
                .ForMember(v => v.CustomerId, opt => opt.Ignore())
                .ForMember(vr => vr.Invoices, opt => opt.MapFrom(v => v.Invoices.Select(vf => new Invoice { Date = vf.Date })));

        }
    }
}
