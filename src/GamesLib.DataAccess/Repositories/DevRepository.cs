using Microsoft.EntityFrameworkCore;
using GamesLib.DataAccess.Context;
using GamesLib.DataAccess.Model;

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

    public async Task<bool> ExistById(int id)
    {
        var result = await _context.Devs.CountAsync(x => x.Id == id);
        return result == 1;
    }
}