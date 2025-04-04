using Microsoft.Extensions.Configuration;
using Google.Apis.Auth.OAuth2;
using Google.Apis.PeopleService.v1;
using Google.Apis.Services;

namespace ContentRecommendationSystem;
public class GoogleAuth
{
    private readonly IConfiguration _configuration;

    public GoogleAuth(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<string> AuthenticateUserAsync()
    {
        var clientId = _configuration["Google:ClientId"];
        var clientSecret = _configuration["Google:ClientSecret"];
        var redirectUri = _configuration["Google:RedirectUri"];

        var credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
            new ClientSecrets
            {
                ClientId = clientId,
                ClientSecret = clientSecret
            },
            new[] { "https://www.googleapis.com/auth/userinfo.profile", "https://www.googleapis.com/auth/userinfo.email" },
            "user", // Идентификатор для пользователя
            CancellationToken.None
        );

        // Получение данных о пользователе (например, его имя)
        var peopleService = new PeopleServiceService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
            ApplicationName = "ContentRecommendationSystem",
        });

        var profile = await peopleService.People.Get("people/me").ExecuteAsync();

        // Возвращаем имя пользователя
        return profile.Names?.FirstOrDefault()?.DisplayName ?? "Неизвестное имя";
    }
}


