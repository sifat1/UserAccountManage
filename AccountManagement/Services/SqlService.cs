using System.Data;
using System.Data.SqlClient;


namespace AccountingSystem.Services
{
    public class SqlService
    {
        private readonly IConfiguration _config;

        public SqlService(IConfiguration config)
        {
            _config = config;
        }

        public List<T> ExecuteReader<T>(string procName, params SqlParameter[] parameters) where T : new()
        {
            var result = new List<T>();
            using var con = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            using var cmd = new SqlCommand(procName, con) { CommandType = CommandType.StoredProcedure };

            if (parameters != null) cmd.Parameters.AddRange(parameters);

            con.Open();
            using var reader = cmd.ExecuteReader();
            var props = typeof(T).GetProperties();

            while (reader.Read())
            {
                var obj = new T();
                foreach (var prop in props)
                {
                    if (!reader.IsDBNull(reader.GetOrdinal(prop.Name)))
                        prop.SetValue(obj, reader[prop.Name]);
                }
                result.Add(obj);
            }

            return result;
        }

        public void ExecuteNonQuery(string procName, params SqlParameter[] parameters)
        {
            using var con = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            using var cmd = new SqlCommand(procName, con) { CommandType = CommandType.StoredProcedure };

            if (parameters != null) cmd.Parameters.AddRange(parameters);

            con.Open();
            cmd.ExecuteNonQuery();
        }
    }
}