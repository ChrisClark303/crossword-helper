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

        protected NpgsqlConnection Connect()
        {
            //TODO : This needs to be injected in really.
            string strConnString = "User Id=user;Password=password;Host=DESKTOP-16C6RAL;Port=5432;Database=crossword_helper;Integrated Security=True";
            NpgsqlConnection conn = new NpgsqlConnection(strConnString);
            conn.Open();
            return conn;
        }

        protected IEnumerable<T> Query<T>(string cmdText, Func<NpgsqlDataReader, T> resultAction, params NpgsqlParameter[]? parameters)
        {
            var executor = new RepositoryExecutor(_connectionStrings);
            return executor
                .Connect()
                .WithFunction(cmdText)
                .WithParams(parameters)
                .Query<T>(resultAction);
        }

        protected void Execute(string cmdText, params NpgsqlParameter[]? parameters)
        {
            new RepositoryExecutor(_connectionStrings)
                .Connect()
                .WithProc(cmdText)
                .WithParams(parameters)
                .Execute();
        }
    }
}