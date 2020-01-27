using System;
using System.ComponentModel.DataAnnotations;

namespace WellCare.Models
{
    public class UserDetails : BaseModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        public string Gender { get; set; }

        [Required]
        public string RoleId { get; set; }

        [Required]
        public DateTime? DateOfBirth { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }

    }
}
