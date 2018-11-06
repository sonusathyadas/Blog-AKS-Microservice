using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BlogService.Models
{
    
    public class Blog:EntityBase
    {

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Author { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime PostedDate { get; set; }

        public string Keywords { get; set; }
    }
}
