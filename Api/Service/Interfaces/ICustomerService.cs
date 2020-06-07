using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Entities;
using Api.Entities.Helpers;
using Api.Helpers;

namespace Api.Service.Interfaces
{
    public interface ICustomerService
    {
        Task<IReadOnlyList<Customer>> GetCustomersAsync();

        Task<Customer> GetCustomerById(int id);
        Task<Pagination<Customer>> FindCustomersAsync(CustomerParams customerParams);

        Task<Customer> AddCustomerAsync(Customer customer);
        Task<bool> UpdateCustomer(int id, Customer customer);
        Task<bool> DeleteCustomer(int id);
    }
}