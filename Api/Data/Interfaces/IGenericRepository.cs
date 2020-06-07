using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Entities;

namespace Api.Data.Interfaces
{
    public interface IGenericRepository<T> where T : EntityBase
    {
        Task<IReadOnlyList<T>> GetListAsync();

        Task<T> GetByIdAsync(int id);

        Task<IReadOnlyList<T>> FindListAsync(ISpecification<T> specification);

        Task<T> FindAsync(ISpecification<T> specification);

        Task<int> CountAsync(ISpecification<T> specification);

        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}