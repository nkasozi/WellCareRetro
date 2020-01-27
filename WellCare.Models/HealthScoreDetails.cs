using System;
using System.Collections.Generic;
using System.Text;

namespace WellCare.Models
{
    public class HealthScoreDetails : BaseModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public float Weight { get; set; }
        public string BloodPressure { get; set; }
    }
}
