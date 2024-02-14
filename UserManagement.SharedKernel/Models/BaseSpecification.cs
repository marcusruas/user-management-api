using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UserManagement.SharedKernel.Retornos.Models;

namespace UserManagement.SharedKernel
{
    public abstract class BaseSpecification<T>
    {
        /// <summary>
        /// Instantiates the specification class to obtain all records of the specified generic entity.
        /// </summary>
        public BaseSpecification()
        {
            Includes = new List<Expression<Func<T, object>>>();
        }

        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
            Includes = new List<Expression<Func<T, object>>>();
        }

        public Expression<Func<T, bool>> Criteria { get; }
        public List<Expression<Func<T, object>>> Includes { get; }
        public Expression<Func<T, object>> OrderBy { get; private set; }
        public Expression<Func<T, object>> OrderByDescending { get; private set; }

        protected void AddInclude(Expression<Func<T, object>> include)
        {
            Includes.Add(include);
        }
        protected void SetOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }
        protected void SetOrderByDescending(Expression<Func<T, object>> orderByExpression)
        {
            OrderByDescending = orderByExpression;
        }
    }
}
