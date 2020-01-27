using System;
using System.Collections.Generic;
using System.Text;
using WellCare.Repositories.Interface;

namespace WellCare.Repositories.Entities
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string RoleId { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public string CreatedById { get; set; }
    }
}
