using Api.Entities;
using Api.Entities.Helpers;

namespace Api.Data.Specification
{
    public class CustomerFilterForCountSpecification : BaseSpecification<Customer>
    {
        public CustomerFilterForCountSpecification(CustomerParams customerParams) : base(c =>
              (string.IsNullOrEmpty(customerParams.Search) || c.Firstname.ToLower().Contains(customerParams.Search))
        )
        {
        }
    }
}