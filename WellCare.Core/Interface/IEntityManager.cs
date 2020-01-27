using System;
using System.Collections.Generic;
using System.Text;
using WellCare.Models;

namespace WellCare.Core.Interface
{
    public interface IEntityManager<DetailsModelClass, CollectionsModelClass, UniqueIdentifierClass>
    {
        Status Save(DetailsModelClass details);

        DetailsModelClass Get(UniqueIdentifierClass id);

        ICollection<CollectionsModelClass> List();

        Status Remove(UniqueIdentifierClass id);

    }
}
