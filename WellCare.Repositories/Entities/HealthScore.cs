using System;
using System.Collections.Generic;
using System.Text;
using WellCare.Repositories.Interface;

namespace WellCare.Repositories.Entities
{
    public class HealthScore : IEntity
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Weight { get; set; }
        public string BloodPressure { get; set; }
        public string BMI { get; set; }
        public float HealthScoreValue { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
