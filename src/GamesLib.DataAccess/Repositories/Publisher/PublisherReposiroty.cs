using Microsoft.EntityFrameworkCore;
using GamesLib.DataAccess.Context;
using GamesLib.DataAccess.Model;
using GamesLib.DataAccess.Repositories.Base;

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

    public async Task<int> AddAsync(string title, string description)
    {
        var entity = new Publisher {Title = title, Description = description};
        var result = await AddAsync(entity);
        return result.Id;
    }

    public async Task<bool> ExistById(int id)
    {
        var result = await _context.Publishers.CountAsync(x => x.Id == id);
        return result == 1;
    }

    public async Task<bool> ExistByTitle(string title)
    {
        var result = await _context.Publishers.CountAsync(x => x.Title == title);
        return result > 0;
    }

    public async Task<int> UpdateAsync(int id, string title, string description)
    {
        var publisher = new Publisher {Id = id, Title = title, Description = description};
        _context.ChangeTracker.Clear();
        _context.Attach(publisher);
        await UpdateAsync(publisher);
        _context.ChangeTracker.Clear();
        return id;
    }
}