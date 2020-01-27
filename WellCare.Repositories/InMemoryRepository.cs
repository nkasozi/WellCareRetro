using System;
using System.Collections.Generic;
using System.Linq;
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

        public IQueryable<T> AsQuery()
        {
            return AllItems.AsQueryable();
        }

        public void Add(T item)
        {
            if (!AllItems.Contains(item)) AllItems.Add(item);
        }

        public void Update(T item)
        {
            int index = AllItems.BinarySearch(item);
            if (index < 0) return;
            AllItems[index] = item;
        }

        public void Remove(T item)
        {
            AllItems.Remove(item);
        }
    }
}
