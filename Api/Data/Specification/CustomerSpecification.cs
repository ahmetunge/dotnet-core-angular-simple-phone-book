using System;
using System.Linq.Expressions;
using Api.Entities;
using Api.Entities.Helpers;

namespace Api.Data.Specification
{
    public class CustomerSpecification : BaseSpecification<Customer>
    {
        public CustomerSpecification(int id) : base(c => c.Id == id)
        {
        }

        public CustomerSpecification(CustomerParams customerParams) : base(c =>
              (string.IsNullOrEmpty(customerParams.Search) || c.Firstname.ToLower().Contains(customerParams.Search))
        )
        {
            ApplyPaging(customerParams.PageSize * (customerParams.PageIndex - 1), customerParams.PageSize);
        }
    }
}