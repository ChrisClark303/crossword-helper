using Npgsql;

namespace CrosswordHelper.Data.Postgres
{
    public interface IRepositoryExecutor
    {
        RepositoryExecutor Connect();
        void Dispose();
        void Execute();
        IEnumerable<T> Query<T>(Func<NpgsqlDataReader, T> resultReader);
        RepositoryExecutor WithFunction(string functionName);
        RepositoryExecutor WithParams(params NpgsqlParameter[]? parameters);
        RepositoryExecutor WithProc(string procName);
    }
}