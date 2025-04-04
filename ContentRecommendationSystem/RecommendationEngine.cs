
namespace ContentRecommendationSystem;

public class RecommendationEngine
{
    private readonly DatabaseService _databaseService;

    public RecommendationEngine(DatabaseService databaseService)
    {
        _databaseService = databaseService;
    }

    // Метод для генерации рекомендаций
    public IEnumerable<Content> GenerateRecommendations(User user)
    {
        // Логика для генерации рекомендаций, например, по категории, интересам или истории активности пользователя
        var userPreferences = _databaseService.GetUserPreferencesByUserId(user.UserId).Result;
        var content = _databaseService.GetContentByCategoryAsync(userPreferences.PreferredCategory).Result;

        return content;
    }
}
