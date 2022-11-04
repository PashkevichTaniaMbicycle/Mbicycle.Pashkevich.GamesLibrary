using GamesLib.DataAccess.Model;

namespace GamesLib.DataAccess.Repositories;

public interface IGameRepository : IRepository<Game>
{
    ICollection<Game> GetBy();
}