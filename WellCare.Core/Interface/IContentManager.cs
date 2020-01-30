using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WellCare.Models;

namespace WellCare.Core.Interface
{
    public interface IContentManager:IEntityManager<ContentDetails,ContentListItem,int>
    {
        Task<ICollection<ContentListItem>> List(FilterResultsRequest filter);
    }
}
