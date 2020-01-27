using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WellCare.Models
{
    public class UserListItem
    {
        [Required]
        public string UserId { get; set; }


        [Required]
        public string Name { get; set; }

    }
}
