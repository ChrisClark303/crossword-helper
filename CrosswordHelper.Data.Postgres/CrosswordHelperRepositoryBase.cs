using Npgsql;

namespace CrosswordHelper.Data.Postgres
{
    public class CrosswordHelperRepositoryBase
    {
        protected NpgsqlConnection Connect()
        {
            string strConnString = "User Id=user;Password=password;Host=DESKTOP-16C6RAL;Port=5432;Database=crossword_helper;Integrated Security=True";
            NpgsqlConnection conn = new NpgsqlConnection(strConnString);
            conn.Open();
            return conn;
        }
    }
}