using System;
using System.Collections.Generic;
using System.Text;

namespace WellCare.Models
{
    public class HealthScoreListItem : BaseModel
    {
        public int Id { get; set; }
        public float HealthScoreValue { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
