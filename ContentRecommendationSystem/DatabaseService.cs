using Dapper;
using Npgsql;

namespace ContentRecommendationSystem;

public class DatabaseService
{
    private readonly string _connString;

    public DatabaseService(string connString)
    {
        _connString = connString;
    }

    // Метод для получения соединения с базой данных
    private NpgsqlConnection GetConnection()
    {
        var conn = new NpgsqlConnection(_connString);
        conn.Open();
        return conn;
    }

    // Метод для получения пользователя по ID
    public async Task<User> GetUserByIdAsync(int userId)
    {
        using (var conn = GetConnection())
        {
            var query = "SELECT * FROM Users WHERE UserId = @UserId";
            var user = await conn.QueryFirstOrDefaultAsync<User>(query, new { UserId = userId });
            return user;
        }
    }

    // Метод для получения контента по категории
    public async Task<IEnumerable<Content>> GetContentByCategoryAsync(string category)
    {
        using (var conn = GetConnection())
        {
            var query = "SELECT * FROM Content WHERE Category = @Category";
            var content = await conn.QueryAsync<Content>(query, new { Category = category });
            return content;
        }
    }

    public async Task<UserPreferences> GetUserPreferencesByUserId(int userId)
    {
        using (var conn = GetConnection())
        {
            var query = "SELECT * FROM UserPreferences WHERE UserId = @UserId";
            var userPreferences = await conn.QueryFirstOrDefaultAsync<UserPreferences>(query, new { UserId = userId });
            return userPreferences;
        }
    }

    // Метод для инициализации базы данных
    public void InitializeDatabase()
    {
        using (var conn = GetConnection())
        {
            Console.WriteLine("Подключение установлено.");
        }
    }
}
