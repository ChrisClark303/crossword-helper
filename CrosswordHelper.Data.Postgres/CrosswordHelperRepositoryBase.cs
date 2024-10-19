using Npgsql;
using System.Data;

namespace CrosswordHelper.Data.Postgres
{
    public class CrosswordHelperRepositoryBase
    {
        private readonly IConnectionStrings _connectionStrings;

        public CrosswordHelperRepositoryBase(IConnectionStrings connectionStrings)
        {
            _connectionStrings = connectionStrings;
        }

        protected IEnumerable<T> Query<T>(string cmdText, Func<NpgsqlDataReader, T> resultAction, params NpgsqlParameter[]? parameters)
        {
            return new QueryBuilder(_connectionStrings)
                .Connect()
                .WithFunction(cmdText)
                .WithParams(parameters)
                .Query<T>(resultAction);
        }

        protected void Execute(string cmdText, params NpgsqlParameter[]? parameters)
        {
            new QueryBuilder(_connectionStrings)
                .Connect()
                .WithProc(cmdText)
                .WithParams(parameters)
                .Execute();
        }

        protected void Test()
        {
            new QueryBuilder(_connectionStrings)
                    .Connect()
                    .WithFunction("test")
                    .Query(reader => reader.GetData(1));
        }
    }
}