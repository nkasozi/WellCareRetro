using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WellCare.Repositories.Interface
{
    public interface IBaseRepository<T> where T : IEntity
    {
        IQueryable<T> AsQuery();
        void Add(T item);
        void Update(T item);
        void Remove(T item);
    }
}
