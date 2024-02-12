using UserManagement.SharedKernel.Retornos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Domain.Users.Entities
{
    public class Role : Table
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
