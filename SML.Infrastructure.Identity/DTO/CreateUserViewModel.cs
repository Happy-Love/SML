using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SML.Infrastructure.Identity.DTO
{
    public class CreateUserViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Year { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
