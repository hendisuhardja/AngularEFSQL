using AngularEFSQL.Data;
using AngularEFSQL.Dto;
using AngularEFSQL.Persistence.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AngularEFSQL.Controllers
{
    [Route("/api/customers")]
    public class CustomersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomersController(IUnitOfWork unitOfWork, ICustomerRepository customerRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] SaveCustomerDto saveCustomerDto)
        {
            //throw new Exception();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var customer = _mapper.Map<SaveCustomerDto, Customer>(saveCustomerDto);

            await _customerRepository.Add(customer);
            await _unitOfWork.CompleteAsync();

            customer = await _customerRepository.GetCustomer(customer.CustomerId);

            var result = _mapper.Map<Customer, CustomerDto>(customer);
            return Ok(result);

        }
               

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] SaveCustomerDto saveCustomerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var customer = await _customerRepository.GetCustomer(id);

            if (customer == null)
            {
                ModelState.AddModelError("CustomerId", "Invalid Customer Id.");
                return BadRequest(ModelState);
            }
            _mapper.Map<SaveCustomerDto, Customer>(saveCustomerDto, customer);

            await _unitOfWork.CompleteAsync();

            customer = await _customerRepository.GetCustomer(id);

            var result = _mapper.Map<Customer, CustomerDto>(customer);
            return Ok(result);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _customerRepository.GetCustomer(id, includeRelated: false);
            if (customer == null)
                return NotFound();

            _customerRepository.Remove(customer);

            await _unitOfWork.CompleteAsync();

            return Ok(id);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer(int id)
        {

            var customer = await _customerRepository.GetCustomer(id);
            if (customer == null)
                return NotFound();

            var result = _mapper.Map<Customer, CustomerDto>(customer);

            return Ok(result);
        }

        [HttpGet]
        public async Task<QueryResultDto<CustomerDto>> GetCustomers(CustomerQueryDto filterDto)
        {
            var filter = _mapper.Map<CustomerQueryDto, CustomerQuery>(filterDto);
            var customers = await _customerRepository.GetCustomers(filter);

            return _mapper.Map<QueryResultDto<Customer>, QueryResultDto<CustomerDto>>(customers);
        }
    }
}