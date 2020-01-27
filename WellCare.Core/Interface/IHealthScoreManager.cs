using System;
using System.Collections.Generic;
using System.Text;
using WellCare.Models;

namespace WellCare.Core.Interface
{
    public interface IHealthScoreManager: IEntityManager<HealthScoreDetails, HealthScoreListItem, int>
    {
    }
}
