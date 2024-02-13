using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.SharedKernel.Messaging;
using UserManagement.SharedKernel.Returns.Handlers;

namespace UserManagement.SharedKernel.Persistence.SQL
{
    public abstract class StandardSqlRepository<TContext> : StandardSqlRepository where TContext : StandardContext<TContext>
    {
        public StandardSqlRepository(IMessaging messaging, IConfiguration configuration, ILogger<StandardSqlRepository<TContext>> logger, TContext context) : base(messaging, configuration, logger)
        {
            Context = context;
        }

        protected readonly TContext Context;

        /// <summary>
        /// Adds an entity to the database. If the <see cref="DbContext.SaveChangesAsync"/> method returns -1 or an exception occurs in the process,
        /// a message of type <see cref="MessageType.Error"/> will be added to the messaging system, and -1 will be returned from this operation.
        /// </summary>
        /// <returns>The number of records affected by this operation.</returns>
        public async Task<int> AddEntity<T>(T entity)
        {
            try
            {
                await Context.AddAsync(entity);
                int affectedRows = await Context.SaveChangesAsync();

                if (affectedRows < 0)
                    Messaging.AddErrorMessage("This operation could not be performed at the moment. Please try again later.");

                return affectedRows;
            }
            catch (Exception ex)
            {
                Messaging.AddErrorMessage("An error occurred while performing this operation.");
                Messaging.AddErrorMessage(ex.ToString());

                return -1;
            }
        }

        /// <summary>
        /// Adds more than one entity to the database. If the <see cref="DbContext.SaveChangesAsync"/> method returns -1 or an exception occurs in the process,
        /// a message of type <see cref="MessageType.Error"/> will be added to the messaging system, and -1 will be returned from this operation.
        /// </summary>
        /// <returns>The number of records affected by this operation.</returns>
        public async Task<int> AddEntity<T>(List<T> entities)
        {
            try
            {
                await Context.AddRangeAsync(entities);
                int affectedRows = await Context.SaveChangesAsync();

                if (affectedRows < 0)
                    Messaging.AddErrorMessage("This operation could not be performed at the moment. Please try again later.");

                return affectedRows;
            }
            catch (Exception ex)
            {
                Messaging.AddErrorMessage("An error occurred while performing this operation.");
                Messaging.AddErrorMessage($"Error details: {ex}");

                return -1;
            }
        }

        /// <summary>
        /// Updates an entity in the database. If the <see cref="DbContext.SaveChangesAsync"/> method returns -1 or an exception occurs in the process,
        /// a message of type <see cref="MessageType.Error"/> will be added to the messaging system, and -1 will be returned from this operation.
        /// </summary>
        /// <returns>The number of records affected by this operation.</returns>
        public async Task<int> UpdateEntity<T>(T entity)
        {
            try
            {
                Context.Update(entity);
                int affectedRows = await Context.SaveChangesAsync();

                if (affectedRows < 0)
                    Messaging.AddErrorMessage("This operation could not be performed at the moment. Please try again later.");

                return affectedRows;
            }
            catch (Exception ex)
            {
                Messaging.AddErrorMessage("An error occurred while performing this operation.");
                Messaging.AddErrorMessage(ex.ToString());

                return -1;
            }
        }

        /// <summary>
        /// Updates more than one entity in the database. If the <see cref="DbContext.SaveChangesAsync"/> method returns -1 or an exception occurs in the process,
        /// a message of type <see cref="MessageType.Error"/> will be added to the messaging system, and -1 will be returned from this operation.
        /// </summary>
        /// <returns>The number of records affected by this operation.</returns>
        public async Task<int> UpdateEntity<T>(List<T> entities)
        {
            try
            {
                Context.UpdateRange(entities);
                int affectedRows = await Context.SaveChangesAsync();

                if (affectedRows < 0)
                    Messaging.AddErrorMessage("This operation could not be performed at the moment. Please try again later.");

                return affectedRows;
            }
            catch (Exception ex)
            {
                Messaging.AddErrorMessage("An error occurred while performing this operation.");
                Messaging.AddErrorMessage($"Error details: {ex}");

                return -1;
            }
        }

        /// <summary>
        /// Deletes an entity from the database. If the <see cref="DbContext.SaveChangesAsync"/> method returns -1 or an exception occurs in the process,
        /// a message of type <see cref="MessageType.Error"/> will be added to the messaging system, and -1 will be returned from this operation.
        /// </summary>
        /// <returns>The number of records affected by this operation.</returns>
        public async Task<int> DeleteEntity<T>(T entity)
        {
            try
            {
                Context.Remove(entity);
                int affectedRows = await Context.SaveChangesAsync();

                if (affectedRows < 0)
                    Messaging.AddErrorMessage("This operation could not be performed at the moment. Please try again later.");

                return affectedRows;
            }
            catch (Exception ex)
            {
                Messaging.AddErrorMessage("An error occurred while performing this operation.");
                Messaging.AddErrorMessage(ex.ToString());

                return -1;
            }
        }

        /// <summary>
        /// Deletes more than one entity from the database. If the <see cref="DbContext.SaveChangesAsync"/> method returns -1 or an exception occurs in the process,
        /// a message of type <see cref="MessageType.Error"/> will be added to the messaging system, and -1 will be returned from this operation.
        /// </summary>
        /// <returns>The number of records affected by this operation.</returns>
        public async Task<int> DeleteEntity<T>(List<T> entities)
        {
            try
            {
                Context.RemoveRange(entities);
                int affectedRows = await Context.SaveChangesAsync();

                if (affectedRows < 0)
                    Messaging.AddErrorMessage("This operation could not be performed at the moment. Please try again later.");

                return affectedRows;
            }
            catch (Exception ex)
            {
                Messaging.AddErrorMessage("An error occurred while performing this operation.");
                Messaging.AddErrorMessage($"Error details: {ex}");

                return -1;
            }
        }

        /// <summary>
        /// Performs a query on the provided context using a member inherited from the class <see cref="BaseSpecification{T}"/>
        /// </summary>
        /// <returns>A list of entities of the specified type that meet the criteria specified in the class <see cref="BaseSpecification{T}"/></returns>
        public async Task<List<T>> QueryWithSpecification<T>(BaseSpecification<T> specification) where T : class
        {
            var query = AddSpecification(specification);
            return await query.ToListAsync();
        }

        /// <summary>
        /// Performs a query on the provided context using a member inherited from the class <see cref="BaseSpecification{T}"/> and returns
        /// a list wrapped in the <see cref="PaginatedList{T}"/> object. If any error occurs, a message 
        /// of type <see cref="MessageType.Error"/> will be added to the messaging system, and null will be returned from this operation.
        /// </summary>
        /// <returns>A list of entities of the specified type that meet the criteria specified in the class <see cref="BaseSpecification{T}"/></returns>
        public async Task<PaginatedList<T>> QueryWithSpecification<T>(PaginatedBaseSpecification<T> specification) where T : class
        {
            if (specification.Page == 0 || specification.RecordsPerPage == 0)
                Messaging.ReturnErrorMessage("Query using pagination cannot have Records per Page or Page as 0.");

            var query = AddSpecification(specification);
            return await PaginatedList<T>.CreateAsync(Context.Set<T>(), specification.Page, specification.RecordsPerPage);
        }

        private IQueryable<T> AddSpecification<T>(BaseSpecification<T> specification) where T : class
        {
            var query = Context.Set<T>().AsQueryable();

            if (specification.Criteria != null)
                query = query.Where(specification.Criteria);

            if (specification.OrderBy != null)
                query = query.OrderBy(specification.OrderBy);

            if (specification.OrderByDescending != null)
                query = query.OrderByDescending(specification.OrderByDescending);

            query = specification.Includes.Aggregate(query, (current, include) => current.Include(include));

            return query;
        }

        /// <summary>
        /// Gets the first record of the entity table in question.
        /// </summary>
        /// <returns></returns>
        public async Task<T> FirstOrDefaultAsync<T>() where T : class
            => await Context.Set<T>().FirstOrDefaultAsync();

        /// <summary>
        /// Gets all the records of the entity table in question
        /// </summary>
        public async Task<List<T>> ToListAsync<T>() where T : class
            => await Context.Set<T>().ToListAsync();

        /// <summary>
        /// Gets all the records of the entity table in question in the form of <see cref="PaginatedList{T}"/>
        /// </summary>
        /// <param name="page">Record page</param>
        /// <param name="recordsPerPage">Number of records per page</param>
        /// <returns></returns>
        public async Task<PaginatedList<T>> ToListAsync<T>(int page, int recordsPerPage) where T : class
        {
            if (page == 0 || recordsPerPage == 0)
                Messaging.ReturnErrorMessage("Query using pagination cannot have Records per Page or Page as 0.");

            return await PaginatedList<T>.CreateAsync(Context.Set<T>(), page, recordsPerPage);
        }
    }
}
