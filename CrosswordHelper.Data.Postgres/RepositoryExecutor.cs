using Npgsql;
using System.Data;

namespace CrosswordHelper.Data.Postgres
{
    public class RepositoryExecutor : IDisposable
    {
        private NpgsqlConnection _conn;
        private NpgsqlCommand _cmd;
        private readonly string _connString = "User Id=user;Password=password;Host=DESKTOP-16C6RAL;Port=5432;Database=crossword_helper;Integrated Security=True";

        public RepositoryExecutor Connect()
        {
            _conn = new NpgsqlConnection(_connString);
            _conn.Open();
            return this;
        }

        public RepositoryExecutor WithProc(string procName)
        {
            _cmd = new(procName, _conn)
            {
                CommandType = CommandType.Text
            };
            return this;
        }

        public RepositoryExecutor WithFunction(string functionName)
        {
            _cmd = new(functionName, _conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            return this;
        }

        public RepositoryExecutor WithParams(params NpgsqlParameter[]? parameters)
        {
            if (parameters != null)
            {
                _cmd.Parameters.AddRange(parameters);
            }
            return this;
        }

        public IEnumerable<T> Query<T>(Func<NpgsqlDataReader, T> resultReader)
        {
            NpgsqlDataReader reader = _cmd.ExecuteReader();
            while (reader.Read())
            {
                yield return resultReader(reader);
            }
            _conn.Dispose();
        }

        public void Execute()
        {
            _cmd.ExecuteNonQuery();
            _conn.Dispose();
        }

        public void Dispose()
        {
            _conn?.Dispose();
        }
    }
}