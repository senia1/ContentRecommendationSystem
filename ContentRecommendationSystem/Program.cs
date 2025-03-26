
using Microsoft.Extensions.Configuration;
using Npgsql;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

string connString = configuration.GetConnectionString("DefaultConnection");

using (var conn = new NpgsqlConnection(connString))
{
    conn.Open();
    Console.WriteLine("Подключение установлено.");
}