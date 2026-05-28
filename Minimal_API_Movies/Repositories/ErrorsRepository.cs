using Dapper;
using Microsoft.Data.SqlClient;
using Minimal_API_Movies.Entities;
using System.Data;

namespace Minimal_API_Movies.Repositories
{
    public class ErrorsRepository : IErrorsRepository
    {
        private readonly string connectionString;

        public ErrorsRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public async Task<Guid> Create(Error error)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                error.Id = Guid.NewGuid();
                await connection.ExecuteAsync("Excecute_Create", new
                {
                    error.Id,
                    error.Message,
                    error.StackTrace,
                    error.Date
                },
                commandType: CommandType.StoredProcedure);
                return error.Id;
            }
        }
    }
}
