using System;
using System.Linq.Expressions;
using Api.Entities;

namespace Api.Data.Interfaces
{
    public interface ISpecification<T> where T : EntityBase
    {
        Expression<Func<T, bool>> Criteria { get; }

        int Take { get; }
        int Skip { get; }
        bool IsPagingEnabled { get; }

    }
}