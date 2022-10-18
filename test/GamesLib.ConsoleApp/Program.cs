using GamesLib.DataAccess.Context;
using GamesLib.DataAccess.Model;
using GamesLib.DataAccess.Repositories;

using (var context = new GamesLibContext())
{
    var game1 = new Game { Title = "The Last Of Us Part I" };
    var game2 = new Game { Title = "Dishonored" };
    
    var gameRepository = new GameRepository(context);

    gameRepository.Add(game1);
    gameRepository.Add(game2);

    foreach (var item in gameRepository.Get())
    {
        Console.WriteLine($"{item.Id} - {item.Title} ");
    }
}