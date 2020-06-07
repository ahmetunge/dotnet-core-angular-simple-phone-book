using System.Linq;
using Api.Data.Interfaces;
using Api.Entities;

namespace Api.Data.Specification
{
    public class SpecificationAnalyzer<T> where T : EntityBase
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> specification)
        {
            var query = inputQuery;
            if (specification.Criteria != null)
            {
                query = inputQuery.Where(specification.Criteria);
            }

            if (specification.IsPagingEnabled)
            {
                query = query.Skip(specification.Skip).Take(specification.Take);
            }

            return query;
        }
    }
}