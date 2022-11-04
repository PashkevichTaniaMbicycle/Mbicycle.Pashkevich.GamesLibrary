using GamesLib.DataAccess.Context;
using GamesLib.DataAccess.Model;

namespace GamesLib.DataAccess.Repositories;

public class DevRepository : Repository<Dev>, IDevRepository
{
    public DevRepository(GamesLibContext context) : base(context)
    {
    }

    protected override Dev CreateEntity(int id)
    {
        return new Dev { Id = id };
    }
}