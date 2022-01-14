using Application.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<List<T>> GetAllAsync();

        Task<T> GetByIdAsync(int id);

        Task<T> AddAsync(T t);

        Task UpdateAsync(T t);

        Task<bool> DeleteAsync(int id);
    }
}