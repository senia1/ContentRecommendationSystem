
using ContentRecommendationSystem;
using Microsoft.Extensions.Configuration;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

// Получение строки подключения из конфигурации
string connString = configuration.GetConnectionString("DefaultConnection");

// Инициализация сервисов
var databaseService = new DatabaseService(connString);
var recommendationEngine = new RecommendationEngine(databaseService);
var googleAuth = new GoogleAuth(configuration);
var consoleInterface = new ConsoleInterface(recommendationEngine, databaseService, googleAuth);

// Инициализация базы данных и вызов меню
databaseService.InitializeDatabase();
consoleInterface.DisplayMenu();


//using ContentRecommendationSystem;
//using Microsoft.Extensions.Configuration;

//var configuration = new ConfigurationBuilder()
//    .SetBasePath(Directory.GetCurrentDirectory())
//    .AddJsonFile("appsettings.json")
//    .Build();

//// Получение строки подключения из конфигурации
//string connString = configuration.GetConnectionString("DefaultConnection");

//// Инициализация сервисов
//var databaseService = new DatabaseService(connString);
//var recommendationEngine = new RecommendationEngine(databaseService);
//var consoleInterface = new ConsoleInterface(recommendationEngine, databaseService);

//// Инициализация базы данных и вызов меню
//databaseService.InitializeDatabase();
//consoleInterface.DisplayMenu();