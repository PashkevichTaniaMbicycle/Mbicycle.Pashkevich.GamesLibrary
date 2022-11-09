using GamesLib.DataAccess.Model;

namespace GamesLib.DataAccess.Repositories;

public interface IDevRepository : IRepository<Dev>
{
    Task<bool> ExistById(int id);
}