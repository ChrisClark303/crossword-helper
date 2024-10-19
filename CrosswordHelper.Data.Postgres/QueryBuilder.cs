
using Npgsql;
using System.Data;
using System.Reflection.Metadata;

namespace CrosswordHelper.Data.Postgres
{
    public class QueryBuilder : IDisposable, IRepositoryExecutor
    {
        public QueryBuilder(IConnectionStrings connectionStrings)
        {
            _connString = connectionStrings.CrosswordHelper;
        }

        private NpgsqlConnection _conn;
        private NpgsqlCommand _cmd;
        private readonly string _connString;
        public QueryBuilder Connect()
        {
            _conn = new NpgsqlConnection(_connString);
            _conn.Open();
            return this;
        }

        public QueryBuilder WithProc(string procName)
        {
            _cmd = new(procName, _conn)
            {
                CommandType = CommandType.Text
            };
            return this;
        }

        public QueryBuilder WithFunction(string functionName)
        {
            _cmd = new(functionName, _conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            return this;
        }

        public QueryBuilder WithParams(params NpgsqlParameter[]? parameters)
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