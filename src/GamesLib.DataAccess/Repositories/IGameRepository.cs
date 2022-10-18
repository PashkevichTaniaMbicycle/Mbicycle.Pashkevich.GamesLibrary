using GamesLib.DataAccess.Context;
using GamesLib.DataAccess.Model;

namespace GamesLib.DataAccess.Repositories
{
    public interface IGameRepository
    {
        Game Add (Game item);
        
        void Delete (Game item);
        
        void Delete(int id);
        
        void Update (Game item);
        
        ICollection<Game> Get();
        
        Game Get(int id);
    }
}