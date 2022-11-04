using Microsoft.EntityFrameworkCore;
using GamesLib.DataAccess.Context;
using GamesLib.DataAccess.Model;

namespace GamesLib.DataAccess.Repositories;

public class GameRepository : Repository<Game>, IGameRepository
{
    private readonly GamesLibContext _context;

    public GameRepository(GamesLibContext context) : base(context)
    {
        _context = context;
    }

    protected override Game CreateEntity(int id)
    {
        return new Game { Id = id };
    }

    public override ICollection<Game> Get()
    {
        return _context.Games
            .Include(x => x.Dev)
            .ToList();
    }

    public ICollection<Game> GetBy()
    {
        throw new NotImplementedException();
    }
}