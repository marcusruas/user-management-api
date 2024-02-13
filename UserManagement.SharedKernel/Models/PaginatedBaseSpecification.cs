using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.SharedKernel
{
    public abstract class PaginatedBaseSpecification<T> : BaseSpecification<T>
    {
        public PaginatedBaseSpecification(int page, int recordsPerPage)
        {
            Page = page;
            RecordsPerPage = recordsPerPage;
        }

        public PaginatedBaseSpecification(Expression<Func<T, bool>> criteria, int page, int recordsPerPage) : base(criteria)
        {
            Page = page;
            RecordsPerPage = recordsPerPage;
        }

        /// <summary>
        /// Page to be searched
        /// </summary>
        public int Page { get; }

        /// <summary>
        /// Number of records per page
        /// </summary>
        public int RecordsPerPage { get; }
    }
}
