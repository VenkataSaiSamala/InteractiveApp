using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace API.Dto
{
    public class LoginDto
    {
        [Required]
        public string userName { get; set; }
        [Required]
        public string password { get; set; }
    }
}
