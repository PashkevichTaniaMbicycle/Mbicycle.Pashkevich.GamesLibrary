using Microsoft.EntityFrameworkCore;
using GamesLib.DataAccess.Context;
using GamesLib.DataAccess.Model;
using GamesLib.DataAccess.Repositories.Base;

namespace GamesLib.DataAccess.Repositories;

public class DevRepository : Repository<Dev>, IDevRepository
{
    private readonly GamesLibContext _context;

    public DevRepository(GamesLibContext context) : base(context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    protected override Dev CreateEntity(int id)
    {
        return new Dev { Id = id };
    }

    public async Task<int> AddAsync(string title, string description)
    {
        var entity = new Dev {Title = title, Description = description};
        var result = await AddAsync(entity);
        return result.Id;
    }

    public async Task<bool> ExistById(int id)
    {
        var result = await _context.Devs.CountAsync(x => x.Id == id);
        return result == 1;
    }

    public async Task<bool> ExistByTitle(string title)
    {
        var result = await _context.Devs.CountAsync(x => x.Title == title);
        return result > 0;
    }

    public async Task<int> UpdateAsync(int id, string title,string description)
    {
        var dev = new Dev {Id = id, Title = title, Description = description};
        _context.ChangeTracker.Clear();
        _context.Attach(dev);
        await UpdateAsync(dev);
        _context.ChangeTracker.Clear();
        return id;
    }
}