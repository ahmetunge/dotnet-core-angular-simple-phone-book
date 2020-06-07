using System.Threading.Tasks;

namespace Api.Data.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> CompleteAsync();
    }
}