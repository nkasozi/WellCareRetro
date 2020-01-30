using System;
using System.Collections.Generic;
using System.Text;
using WellCare.Repositories.Interface;

namespace WellCare.Repositories.Entities
{
    public class Content : IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ContentValue { get; set; }
        public string AuthorId { get; set; }
        public string Category { get; set; }
        public string Type { get; set; }
        public DateTime DateModified { get; set; }
        public DateTime DateCreated { get; set; }
        
    }
}
