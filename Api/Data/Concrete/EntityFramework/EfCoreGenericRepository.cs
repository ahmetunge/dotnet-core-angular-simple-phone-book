using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data.Interfaces;
using Api.Data.Specification;
using Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Concrete.EntityFramework
{
    public class EfCoreGenericRepository<T> : IGenericRepository<T> where T : EntityBase
    {
        private readonly CustomerContext _context;
        public EfCoreGenericRepository(CustomerContext context)
        {
            _context = context;

        }
        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public async Task<int> CountAsync(ISpecification<T> specification)
        {
            return await ApplySpecification(specification).CountAsync();
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task<T> FindAsync(ISpecification<T> specification)
        {
            return await ApplySpecification(specification).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> FindListAsync(ISpecification<T> specification)
        {
            return await ApplySpecification(specification).ToListAsync();
        }



        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> GetListAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public void Update(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> specification)
        {
            return SpecificationAnalyzer<T>.GetQuery(_context.Set<T>(), specification);
        }
    }
}