using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Data.Interfaces;
using Api.Data.Specification;
using Api.Entities;
using Api.Entities.Helpers;
using Api.Helpers;
using Api.Service.Interfaces;

namespace Api.Service.Concrete
{
    public class CustomerService : ICustomerService
    {
        private readonly IGenericRepository<Customer> _customerRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CustomerService(IGenericRepository<Customer> customerRepository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _customerRepository = customerRepository;

        }
        public async Task<IReadOnlyList<Customer>> GetCustomersAsync()
        {
            return await _customerRepository.GetListAsync();
        }

        public async Task<Customer> GetCustomerById(int id)
        {
            var specification = new CustomerSpecification(id);

            return await _customerRepository.FindAsync(specification);
        }

        public async Task<Pagination<Customer>> FindCustomersAsync(CustomerParams customerParams)
        {
            var specification = new CustomerSpecification(customerParams);

            var countSpecification = new CustomerFilterForCountSpecification(customerParams);

            int totalItems = await _customerRepository.CountAsync(countSpecification);

            var data = await _customerRepository.FindListAsync(specification);


            return new Pagination<Customer>(customerParams.PageIndex, customerParams.PageSize, totalItems, data);
        }

        public async Task<Customer> AddCustomerAsync(Customer customer)
        {
            _customerRepository.Add(customer);

            int affectedRow = await _unitOfWork.CompleteAsync();

            if (affectedRow > 0)
            {
                return customer;
            }

            return null;
        }

        public async Task<bool> UpdateCustomer(int id, Customer customer)
        {
            var customerFromDb = await _customerRepository.GetByIdAsync(id);
            if (customer == null)
                return false;


            customerFromDb.Email = customer.Email;
            customerFromDb.Firstname = customer.Firstname;
            customerFromDb.Gender = customer.Gender;
            customerFromDb.Lastname = customer.Lastname;
            customerFromDb.Phone = customer.Phone;


            int affectedRows = await _unitOfWork.CompleteAsync();

            if (affectedRows > 0)
                return true;

            return false;
        }

        public async Task<bool> DeleteCustomer(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);

            if (customer == null)
                return false;

            _customerRepository.Delete(customer);

            if (await _unitOfWork.CompleteAsync() > 0)
                return true;

            return false;
        }
    }
}