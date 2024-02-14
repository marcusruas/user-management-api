using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.SharedKernel.Messaging;

namespace UserManagement.SharedKernel.Persistence.SQL
{
    public abstract class StandardSqlRepository : IRepository
    {
        public StandardSqlRepository(IMessaging messaging, IConfiguration configuration, ILogger<StandardSqlRepository> logger)
        {
            Messaging = messaging;
            Logging = logger;
            Configuration = configuration;

            SetSqlPath();
        }

        protected readonly IMessaging Messaging;
        protected readonly ILogger<StandardSqlRepository> Logging;

        private readonly IConfiguration Configuration;
        private string _sqlFolderPath { get; set; }
        private const string INFRASTRUCTURE_LAYER = "Repositories";
        private const string STANDARD_SQL_FOLDER = "SQL";


        /// <summary>
        /// Obtains the content (SQL query) from the file whose name is provided
        /// 
        /// If the file cannot be found, a message of type <see cref="MessageType.Error"/> will be added to the messaging object and null will be returned.
        /// If there is any type of error attempting to read the file, an error message will be added as in the example above stating what occurred.
        /// </summary>
        /// <param name="fileName">Name of the SQL file to be obtained (the .sql extension is not required)</param>
        protected string GetSqlQueryByFileName(string fileName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(_sqlFolderPath))
                    return null;

                string fixedFileName = fileName.EndsWith(".sql") ? fileName.Replace(".sql", string.Empty) : fileName;
                string[] lines;
                string fileContent = string.Empty;

                string filePath = Path.Combine(_sqlFolderPath, fixedFileName);
                filePath = filePath.Replace("file:\\", "");

                if (!File.Exists(filePath))
                {
                    Logging.LogError("The SQL Query file {fixedFileName} could not be found.", fixedFileName);
                    Messaging.ReturnErrorMessage($"Failed to get the SQL File {fileName}.");
                }

                lines = File.ReadAllLines(filePath);

                foreach (var linha in lines)
                    fileContent += $"{linha} ";

                return fileContent;
            }
            catch (ArgumentNullException)
            {
                Logging.LogError("The SQL File {fileName} is empty.", fileName);
                Messaging.ReturnErrorMessage($"The SQL File {fileName} is empty.");
                return null;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex, "Failed to read the SQL file {fileName}.", fileName);
                Messaging.ReturnErrorMessage("An error has occurred while trying to access the SQL Queries.");
                return null;
            }
        }

        /// <summary>
        /// Executes a user-defined function intended to retrieve data from the database to which the <see cref="SqlConnection"/> points.
        /// 
        /// If an error (exception) occurs in the function, a message of type <see cref="MessageType.Error"/> will be added to the messaging object and an exception will be thrown.
        /// </summary>
        /// <param name="command">the function that will be executed to pass commands to the database</param>
        /// <returns>A task with the result of the function.</returns>
        protected async Task ExecuteSqlCommand(Func<SqlConnection, Task> command, string databaseName)
        {
            try
            {
                using (var connection = CreateSqlConnection(databaseName))
                {
                    connection.Open();
                    await command(connection);
                }
            }
            catch (Exception ex)
            {
                Logging.LogError(ex, "Failed to execute a SQL Operation at database {databaseName}", databaseName);
                Messaging.AddErrorMessage($"An error has occurred while processing your operation. Please try again later.");
            }
        }

        /// <summary>
        /// Creates a new SqlConnection based on the connection string whose key matches the databaseName parameter
        /// </summary>
        protected SqlConnection CreateSqlConnection(string databaseName)
        {
            var connectionString = Configuration.GetConnectionString(databaseName);
            return new SqlConnection(connectionString);
        }

        private void SetSqlPath()
        {
            string raizProjeto = Path.GetDirectoryName(GetType().Assembly.Location) ?? "";
            List<string> namespaces = GetType().Namespace?
                .Split(".")
                .Where(ns => ns != INFRASTRUCTURE_LAYER)
                .ToList() ?? new List<string>();

            _sqlFolderPath = Path.Combine(raizProjeto, string.Join("\\", namespaces), STANDARD_SQL_FOLDER);
            _sqlFolderPath = _sqlFolderPath.Replace("file:\\", "");
        }
    }
}
