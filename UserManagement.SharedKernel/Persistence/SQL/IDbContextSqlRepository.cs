using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.SharedKernel.Returns.Handlers;

namespace UserManagement.SharedKernel.Persistence.SQL
{
    public interface IStandardSqlRepository<TContext> where TContext : DbContext
    {
        /// <summary>
        /// Adds a single entity to the database and returns the number of records affected. If the DbContext.SaveChangesAsync method throws an exception, it will be logged.
        /// </summary>
        /// <returns>The number of records affected by this operation.</returns>
        Task<int> AddEntity<T>(T entity);

        /// <summary>
        /// Adds multiple entities to the database and returns the number of records affected. If the DbContext.SaveChangesAsync method throws an exception, it will be logged.
        /// </summary>
        /// <returns>The number of records affected by this operation.</returns>
        Task<int> AddEntity<T>(List<T> entities);

        /// <summary>
        /// Updates an entity in the database and returns the number of records affected. If the DbContext.SaveChangesAsync method throws an exception, it will be logged.
        /// </summary>
        /// <returns>The number of records affected by this operation.</returns>
        Task<int> UpdateEntity<T>(T entity);

        /// <summary>
        /// Updates more than one entity in the database and returns the number of records affected. If the DbContext.SaveChangesAsync method throws an exception, it will be logged.
        /// </summary>
        /// <returns>The number of records affected by this operation.</returns>
        Task<int> UpdateEntity<T>(List<T> entities);

        /// <summary>
        /// Deletes an entity from the database and returns the number of records affected. If the DbContext.SaveChangesAsync method throws an exception, it will be logged.
        /// </summary>
        /// <returns>The number of records affected by this operation.</returns>
        Task<int> DeleteEntity<T>(T entity);

        /// <summary>
        /// Deletes more than one entity from the database. If the DbContext.SaveChangesAsync method returns -1 or an exception occurs, a message of type MessageType.Error will be added to the messaging system, and -1 will be returned.
        /// </summary>
        /// <returns>The number of records affected by this operation.</returns>
        Task<int> DeleteEntity<T>(List<T> entities);

        /// <summary>
        /// Performs a query on the provided context using a specification. Returns a list of entities of the specified type that meet the criteria.
        /// </summary>
        /// <returns>A list of entities of the specified type.</returns>
        Task<List<T>> QueryAsync<T>(BaseSpecification<T> specification) where T : class;

        /// <summary>
        /// Performs a query on the context using a specification and returns a paginated list of entities. If an error occurs, a message of type MessageType.Error will be added to the messaging system, and null will be returned.
        /// </summary>
        /// <returns>A paginated list of entities of the specified type.</returns>
        Task<PaginatedList<T>> QueryAsync<T>(PaginatedBaseSpecification<T> specification) where T : class;

        /// <summary>
        /// Performs a query on the provided context using a specification. 
        /// </summary>
        /// <returns>Returns if there any any entities that matches the criteria.</returns>
        Task<bool> AnyAsync<T>(BaseSpecification<T> specification) where T : class;

        /// <summary>
        /// Gets the first record of the entity table in question.
        /// </summary>
        /// <returns>The first or default entity.</returns>
        Task<T> FirstOrDefaultAsync<T>() where T : class;

        /// <summary>
        /// Performs a query on the provided context using a specification. 
        /// </summary>
        /// <returns>The first entity that matched the criteria.</returns>
        Task<T> FirstOrDefaultAsync<T>(BaseSpecification<T> specification) where T : class;

        /// <summary>
        /// Gets all the records of the entity table in question.
        /// </summary>
        /// <returns>A list of all entities.</returns>
        Task<List<T>> ToListAsync<T>() where T : class;

        /// <summary>
        /// Gets all the records of the entity table in question in the form of PaginatedList<T>.
        /// </summary>
        /// <param name="page">The current page number.</param>
        /// <param name="recordsPerPage">The number of records per page.</param>
        /// <returns>A paginated list of entities.</returns>
        Task<PaginatedList<T>> ToListAsync<T>(int page, int recordsPerPage) where T : class;
    }

}
