#nullable disable

using DataAccess.Records.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Post : RecordBase
    {
        [Required]
        [StringLength(40)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }
        public DateTime PostedOn { get; set; }

        public int UserId { get; set; } // foreign key for User
        public User User { get; set; }

        public int CategoryId { get; set; } // foreign key for Category
        public Category Category { get; set; }
    }
}
