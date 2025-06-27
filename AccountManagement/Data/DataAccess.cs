using System.Data;
using System.Data.SqlClient;

namespace AccountManagement.Data
{
    public class DataAccess
    {
        private readonly string _connectionString;

        public DataAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<DataTable> ExecuteStoredProcedureAsync(string procedureName, SqlParameter[] parameters)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(procedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    if (parameters != null) command.Parameters.AddRange(parameters);
                    
                    await connection.OpenAsync();
                    var dataTable = new DataTable();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        dataTable.Load(reader);
                    }
                    return dataTable;
                }
            }
        }

        public async Task<int> ExecuteNonQueryStoredProcedureAsync(string procedureName, SqlParameter[] parameters)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(procedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    if (parameters != null) command.Parameters.AddRange(parameters);
                    
                    await connection.OpenAsync();
                    return await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<object> ExecuteScalarStoredProcedureAsync(string procedureName, SqlParameter[] parameters)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(procedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    if (parameters != null) command.Parameters.AddRange(parameters);
                    
                    await connection.OpenAsync();
                    return await command.ExecuteScalarAsync();
                }
            }
        }
    }
}