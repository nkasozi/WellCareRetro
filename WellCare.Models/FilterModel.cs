using System;
using System.Collections.Generic;
using System.Text;

namespace WellCare.Models
{
    public class FilterResultsRequest
    {
        public String Term { get; set; }
        public DateTime MinDate { get; set; }
        public DateTime MaxDate { get; set; }
        public int Page { get; set; }
        public int Limit { get; set; }
    }
}
