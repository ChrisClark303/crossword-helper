using Npgsql;

namespace CrosswordHelper.Data.Postgres
{
    public class CrosswordHelperRepositoryBase
    {
        protected NpgsqlConnection Connect()
        {
            string strConnString = "User Id=postgres;Host=172.25.16.1;Port=5432;Database=crossword_helper;Integrated Security=True";
            NpgsqlConnection conn = new NpgsqlConnection(strConnString);
            conn.Open();
            return conn;
        }
    }
}