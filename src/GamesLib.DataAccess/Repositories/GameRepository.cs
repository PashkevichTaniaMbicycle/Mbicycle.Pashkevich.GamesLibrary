using GamesLib.DataAccess.Context;
using GamesLib.DataAccess.Model;

namespace GamesLib.DataAccess.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly GamesLibContext _context;

        public GameRepository(GamesLibContext context)
        {
            _context = context;
        }

        public Game Add(Game item)
        {
            var result = _context.Games.Add(item);
            _context.SaveChanges();

            return result.Entity;
        }

        public void Delete(Game item)
        {
            _context.Games.Remove(item);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var item = new Game { Id = id };
            _context.Games.Attach(item);
            Delete(item);
        }

        public ICollection<Game> Get()
        {
            return _context.Games.ToList();
        }

        public Game Get(int id)
        {
            var result = _context.Games
                .Where(x => x.Id == id)
                .FirstOrDefault();

            result ??= new Game();

            return result;
        }
        public void Update(Game item)
        {
            _context.Update(item);
            _context.SaveChanges();

        }
    }

}