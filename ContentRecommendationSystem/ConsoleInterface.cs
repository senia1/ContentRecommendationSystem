

namespace ContentRecommendationSystem;

public class ConsoleInterface
{
    private readonly RecommendationEngine _recommendationEngine;
    private readonly DatabaseService _databaseService;
    private readonly GoogleAuth _googleAuth;

    public ConsoleInterface(RecommendationEngine recommendationEngine, DatabaseService databaseService, GoogleAuth googleAuth)
    {
        _recommendationEngine = recommendationEngine;
        _databaseService = databaseService;
        _googleAuth = googleAuth;
    }

    public void DisplayMenu()
    {
        Console.WriteLine("1. Получить рекомендации");
        Console.WriteLine("2. Просмотреть контент");
        Console.WriteLine("3. Авторизация через Google");
        Console.WriteLine("4. Выход");

        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                ShowRecommendations();
                break;
            case "2":
                ShowContent();
                break;
            case "3":
                AuthenticateGoogleUser().Wait(); // Вызываем авторизацию через Google
                break;
            case "4":
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine("Неверный выбор.");
                break;
        }
    }

    private async Task AuthenticateGoogleUser()
    {
        Console.WriteLine("Пожалуйста, авторизуйтесь через Google...");
        var userName = await _googleAuth.AuthenticateUserAsync();
        Console.WriteLine($"Добро пожаловать, {userName}!");
    }

    private void ShowRecommendations()
    {
        var user = GetCurrentUser();
        var recommendations = _recommendationEngine.GenerateRecommendations(user);
        foreach (var content in recommendations)
        {
            Console.WriteLine($"Рекомендованный контент: {content.Title}");
        }
    }

    private void ShowContent()
    {
        var content = _databaseService.GetContentByCategoryAsync("Movies").Result;
        foreach (var item in content)
        {
            Console.WriteLine($"Контент: {item.Title}");
        }
    }

    private User GetCurrentUser()
    {
        // Логика для получения текущего пользователя
        return new User { UserId = 1, Name = "Иван" };
    }
    //private readonly RecommendationEngine _recommendationEngine;
    //private readonly DatabaseService _databaseService;

    //public ConsoleInterface(RecommendationEngine recommendationEngine, DatabaseService databaseService)
    //{
    //    _recommendationEngine = recommendationEngine;
    //    _databaseService = databaseService;
    //}

    //public void DisplayMenu()
    //{
    //    Console.WriteLine("1. Получить рекомендации");
    //    Console.WriteLine("2. Просмотреть контент");
    //    Console.WriteLine("3. Выход");

    //    var choice = Console.ReadLine();

    //    switch (choice)
    //    {
    //        case "1":
    //            ShowRecommendations();
    //            break;
    //        case "2":
    //            ShowContent();
    //            break;
    //        case "3":
    //            Environment.Exit(0);
    //            break;
    //        default:
    //            Console.WriteLine("Неверный выбор.");
    //            break;
    //    }
    //}

    //private void ShowRecommendations()
    //{
    //    var user = GetCurrentUser();
    //    var recommendations = _recommendationEngine.GenerateRecommendations(user);
    //    foreach (var content in recommendations)
    //    {
    //        Console.WriteLine($"Рекомендованный контент: {content.Title}");
    //    }
    //}

    //private void ShowContent()
    //{
    //    var content = _databaseService.GetContentByCategoryAsync("Movies").Result;
    //    foreach (var item in content)
    //    {
    //        Console.WriteLine($"Контент: {item.Title}");
    //    }
    //}

    //private User GetCurrentUser()
    //{
    //    // Логика для получения текущего пользователя
    //    return new User { UserId = 1, Name = "Иван" };
    //}
}
