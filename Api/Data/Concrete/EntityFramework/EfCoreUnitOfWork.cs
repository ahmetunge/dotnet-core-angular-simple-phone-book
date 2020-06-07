using System.Threading.Tasks;
using Api.Data.Interfaces;

namespace Api.Data.Concrete.EntityFramework
{
    public class EfCoreUnitOfWork : IUnitOfWork
    {
        private readonly CustomerContext _context;
        public EfCoreUnitOfWork(CustomerContext context)
        {
            _context = context;

        }
        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}