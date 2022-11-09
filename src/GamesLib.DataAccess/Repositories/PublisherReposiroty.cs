using Microsoft.EntityFrameworkCore;
using GamesLib.DataAccess.Context;
using GamesLib.DataAccess.Model;

namespace GamesLib.DataAccess.Repositories;

public class PublisherRepository : Repository<Publisher>, IPublisherRepository
{
    private readonly GamesLibContext _context;

    public PublisherRepository(GamesLibContext context) : base(context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    protected override Publisher CreateEntity(int id)
    {
        return new Publisher { Id = id };
    }

    public async Task<bool> ExistById(int id)
    {
        var result = await _context.Publishers.CountAsync(x => x.Id == id);
        return result == 1;
    }
}