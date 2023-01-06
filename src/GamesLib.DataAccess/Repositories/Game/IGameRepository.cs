using GamesLib.DataAccess.Model;
using GamesLib.DataAccess.Repositories.Base;

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
    
    Task<bool> ExistById(int id);
    Task<int> UpdateAsync(
        int gameId, 
        int devId, 
        int publisherId, 
        DateTime releaseDate, 
        int rating, 
        string title,
        string description
        );
}