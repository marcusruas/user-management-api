using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.SharedKernel.Retornos.Models;

namespace UserManagement.Domain.Users.Entities
{
    public class User : Table
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string CPF { get; set; }
        public string Password { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
