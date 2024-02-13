using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.SharedKernel.Retornos.Models
{
    public abstract class Table
    {
        public Table()
        {
            Id = Guid.NewGuid();
            CreatedDate = DateTime.Now;
            Deleted = false;
        }

        public Guid Id { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Deleted { get; set; }
    }
}
