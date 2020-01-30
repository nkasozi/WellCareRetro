using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellCare.Repositories.Interface
{
    public interface IBaseRepository<T> where T : IEntity
    {
        Task<IQueryable<T>> AsQueryAsync();
        Task AddAsync(T item);
        Task UpdateAsync(T item);
        Task RemoveAsync(T item);
    }
}
