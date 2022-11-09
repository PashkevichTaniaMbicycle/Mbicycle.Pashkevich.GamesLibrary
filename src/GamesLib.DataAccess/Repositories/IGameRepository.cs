using GamesLib.DataAccess.Model;

namespace GamesLib.DataAccess.Repositories;

public interface IGameRepository : IRepository<Game>
{
    Task<ICollection<Game>> GetAllGamesAsync();
    
    Task<int> AddAsync(
        int devId, 
        int publisherId, 
        DateTime releaseDate, 
        int rating, 
        string title,
        string description
        );
}