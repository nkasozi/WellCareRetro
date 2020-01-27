using System;
using System.Collections.Generic;
using System.Text;

namespace WellCare.Entities
{
    public class User
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public DateTime? DateOfBitrh { get; set; }
        public string Gender { get; set; }
        public string RoleId { get; set; }
    }
}
