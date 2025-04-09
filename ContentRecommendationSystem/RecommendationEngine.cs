
namespace ContentRecommendationSystem;

public class RecommendationEngine
{
    private readonly DatabaseService _databaseService;

    public RecommendationEngine(DatabaseService databaseService)
    {
        _databaseService = databaseService;
    }

    public IEnumerable<Content> GenerateRecommendations(User user)
    {
        var userPreferences = _databaseService.GetUserPreferencesByUserId(user.UserId).Result;
        var content = _databaseService.GetContentByCategoryAsync(userPreferences.PreferredCategory).Result;

        return content;
    }
}
