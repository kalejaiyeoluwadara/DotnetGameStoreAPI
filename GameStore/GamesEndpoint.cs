using GameStore.Api.Dtos;

namespace GameStore.Api.Endpoints;

public static class GamesEndpoint
{
    const string GetGameEndpointName = "GetGame";

    private static readonly List<GameDto> games = new List<GameDto> {
     new (1, "The Witcher 3: Wild Hunt", "RPG", 29.99m, new DateOnly(2015, 5, 19)),
    new (2, "God of War", "Action", 49.99m, new DateOnly(2018, 4, 20)),
    new (3, "Minecraft", "Sandbox", 19.99m, new DateOnly(2011, 11, 18)),
    new (4, "Grand Theft Auto V", "Action-Adventure", 39.99m, new DateOnly(2013, 9, 17)),
    new (5, "Hollow Knight", "Metroidvania", 14.99m, new DateOnly(2017, 2, 24)),
    new (6, "Cyberpunk 2077", "RPG", 59.99m, new DateOnly(2020, 12, 10)),
    };


    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/games");

        // GET all games
        group.MapGet("/", () => games).WithName(GetGameEndpointName);


        // POST game
        group.MapPost("/", (CreateGameDto newGame) =>
        {
            GameDto game = new(
                games.Count + 1,
                newGame.Name,
                newGame.Genre,
                newGame.Price,
                newGame.ReleaseDate
            );
            games.Add(game);
            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
        }).WithParameterValidation();

        // PUT game
        group.MapPut("/{id}", (int id, UpdateGameDto updateGame) =>
        {
            var index = games.FindIndex(game => game.Id == id);
            if (index != -1)
            {
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
        group.MapGet("/{id}", (int id) =>
           {
               GameDto? game = games.Find(game => game.Id == id);
               return game is null ? Results.NotFound() : Results.Ok(game);
           }
         );

        // DELETE game
        group.MapDelete("/{id}", (int id) =>
        {
            games.RemoveAll(game => game.Id == id);
            return Results.NoContent();
        });
        return group;
    }
}
