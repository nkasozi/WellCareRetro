using System;
using System.Collections.Generic;
using System.Text;

namespace WellCare.Entities
{
    public class HealthScore
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public double HeightInMetres { get; set; }
        public double WeightInKg { get; set; }
        public double BMI { get; set; }
        public DateTime CreationDate { get; set; }
        public BloodPressure BP { get; set; }
    }
}
