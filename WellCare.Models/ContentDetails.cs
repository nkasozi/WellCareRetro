using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WellCare.Models
{
    public class ContentDetails : BaseModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string ContentValue { get; set; }

        [Required]
        public string AuthorId { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string Type { get; set; }
    }
}
