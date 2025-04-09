
using ContentRecommendationSystem;
using Microsoft.Extensions.Configuration;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

string connString = configuration.GetConnectionString("DefaultConnection");

var databaseService = new DatabaseService(connString);
var recommendationEngine = new RecommendationEngine(databaseService);
var googleAuth = new GoogleAuth(configuration);
var consoleInterface = new ConsoleInterface(recommendationEngine, databaseService, googleAuth);

databaseService.InitializeDatabase();
consoleInterface.DisplayMenu();
