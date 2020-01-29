using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WellCare.Repositories.Entities;
using WellCare.Repositories.Interface;

namespace WellCare.Repositories
{
    public class InMemoryRepository<T> : IBaseRepository<T> where T : IEntity
    {
        public static List<T> _allItems = new List<T>();

        public List<T> AllItems
        {
            get
            {
                return _allItems;
            }
        }

        public async Task<IQueryable<T>> AsQueryAsync()
        {
            return await Task.Run(() =>
            {
                return AllItems.AsQueryable();
            });
        }

        public async Task AddAsync(T item)
        {
            await Task.Run(() =>
            {
                if (!AllItems.Contains(item))
                    AllItems.Add(item);
            });
        }

        public async Task UpdateAsync(T item)
        {
            await Task.Run(() =>
            {
                int index = AllItems.BinarySearch(item);
                if (index < 0) return;
                AllItems[index] = item;
            });
        }

        public async Task RemoveAsync(T item)
        {
            await Task.Run(() =>
            {
                AllItems.Remove(item);
            });
        }
    }
}
