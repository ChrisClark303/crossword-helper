using Npgsql;

namespace CrosswordHelper.Data.Postgres
{
    public interface IRepositoryExecutor
    {
        QueryBuilder Connect();
        void Dispose();
        void Execute();
        IEnumerable<T> Query<T>(Func<NpgsqlDataReader, T> resultReader);
        QueryBuilder WithFunction(string functionName);
        QueryBuilder WithParams(params NpgsqlParameter[]? parameters);
        QueryBuilder WithProc(string procName);
    }
}