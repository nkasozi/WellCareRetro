using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WellCare.Models;

namespace WellCare.Core.Interface
{
    public interface IEntityManager<DetailsModelClass, CollectionsModelClass, UniqueIdentifierClass>
    {
        Task<Status> SaveAsync(DetailsModelClass details);

        Task<DetailsModelClass> GetByIdAsync(UniqueIdentifierClass id);

        Task<ICollection<CollectionsModelClass>> List();

        Task<Status> RemoveByIdAsync(UniqueIdentifierClass id);

    }
}
