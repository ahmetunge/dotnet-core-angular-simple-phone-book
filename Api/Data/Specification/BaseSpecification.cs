using System;
using System.Linq.Expressions;
using Api.Data.Interfaces;
using Api.Entities;

namespace Api.Data.Specification
{
    public class BaseSpecification<T> : ISpecification<T> where T : EntityBase
    {
        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        public BaseSpecification()
        {

        }
        public Expression<Func<T, bool>> Criteria { get; }

        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool IsPagingEnabled { get; private set; }

        protected void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }
    }
}