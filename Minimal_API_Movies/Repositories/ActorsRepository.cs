using Dapper;
using Microsoft.Data.SqlClient;
using Minimal_API_Movies.Entities;
using System.Data;

namespace Minimal_API_Movies.Repositories
{
    public class ActorsRepository : IActorsRepository
    {
        private readonly string connectionString;

        public ActorsRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public async Task<int> Create(Actor actor)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var procedure = "Actors_Create";
                var id = await connection.QuerySingleAsync<int>(procedure,
                    new { actor.Name, actor.DateOfBirth, actor.Picture }, commandType: CommandType.StoredProcedure);
                actor.Id = id;
                return id;
            }
        }

        public async Task Delete(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var procedure = "Actors_Delete";
                await connection.ExecuteAsync(procedure,
                    new { id }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<bool> Exist(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var procedure = "Actors_Exist";
                var exists = await connection.QuerySingleAsync<bool>(procedure,
                    new { id }, commandType: CommandType.StoredProcedure);
                return exists;
            }
        }

        public async Task<List<Actor>> GetAll()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var procedure = "Actors_GetAll";
                var actors = await connection.QueryAsync<Actor>(procedure, commandType: CommandType.StoredProcedure);
                return actors.ToList();
            }
        }

        public async Task<Actor?> GetById(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var procedure = "Actors_GetById";
                var actor = await connection.QuerySingleOrDefaultAsync<Actor>(procedure,
                    new { id }, commandType: CommandType.StoredProcedure);
                return actor;
            }
        }

        public async Task Update(Actor actor)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var procedure = "Actors_Update";
                await connection.ExecuteAsync(procedure,
                    new { actor.Id, actor.Name, actor.DateOfBirth, actor.Picture }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<List<Actor>> GetByName(string name)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var procedure = "Actors_GetByName";
                var actors = await connection.QueryAsync<Actor>(procedure,
                    new { name }, commandType: CommandType.StoredProcedure);
                return actors.ToList();
            }
        }
    }
}
