using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogUserService.Models
{
    public class BlogUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Key]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
