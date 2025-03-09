using GameStore.Api.Dtos;

var builder = WebApplication.CreateBuilder(args);

var GetGameEndpointName = "GetGame";
var app = builder.Build();

var games = new List<GameDto>([
     new (1, "The Witcher 3: Wild Hunt", "RPG", 29.99m, new DateOnly(2015, 5, 19)),
    new (2, "God of War", "Action", 49.99m, new DateOnly(2018, 4, 20)),
    new (3, "Minecraft", "Sandbox", 19.99m, new DateOnly(2011, 11, 18)),
    new (4, "Grand Theft Auto V", "Action-Adventure", 39.99m, new DateOnly(2013, 9, 17)),
    new (5, "Hollow Knight", "Metroidvania", 14.99m, new DateOnly(2017, 2, 24)),
    new (6, "Cyberpunk 2077", "RPG", 59.99m, new DateOnly(2020, 12, 10)),
]);

// GET all games
app.MapGet("/games", () => games).WithName(GetGameEndpointName);


// POST game
app.MapPost("/games", (CreateGameDto newGame) => 
{
    GameDto game = new(
        games.Count + 1,
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate
    );
    games.Add(game);
    return Results.CreatedAtRoute(GetGameEndpointName,new {id = game.Id},game);
});

// PUT game
app.MapPut("/games/{id}",(int id, UpdateGameDto updateGame)=>
{
    var index = games.FindIndex(game=>game.Id == id);
    if(index != -1){
    games[index] = new GameDto(
        id,
        updateGame.Name,
        updateGame.Genre,
        updateGame.Price,
        updateGame.ReleaseDate
    );
    return Results.NoContent();
    }
    return Results.NotFound();
});

// GET single game
app.MapGet("/games/{id}", (int id) =>
    games.Find(game=>game.Id == id)
 );

app.Run();
