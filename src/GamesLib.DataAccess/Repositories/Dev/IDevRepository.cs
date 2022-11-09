using GamesLib.DataAccess.Model;
using GamesLib.DataAccess.Repositories.Base;

namespace GamesLib.DataAccess.Repositories;

public interface IDevRepository : IRepository<Dev>
{
    Task<int> AddAsync(string title);

    Task<bool> ExistById(int id);

    Task<bool> ExistByTitle(string title);

    Task<int> UpdateAsync(int id, string title);
}