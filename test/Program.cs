using Npgsql;
internal class Program
{
    private static void Main(string[] args)
    {
        string connectionString = "Host=localhost;Port=5432;Database=test;Username=postgres;Password=12345;Maximum Pool Size=4;Minimum Pool Size=1;";

        for (int i = 0; i < 7; i++)
        {
            Console.WriteLine(i.ToString());
            var connection = new NpgsqlConnection(connectionString);            
            connection.Open();

            if (i == 3) {
                connection.Close();
            }
            // Realiza operaciones con la base de datos
        }
    }
}




