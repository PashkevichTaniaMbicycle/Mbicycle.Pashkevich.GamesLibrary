using GamesLib.DataAccess.Model;

namespace GamesLib.DataAccess.Repositories;

public interface IPublisherRepository : IRepository<Publisher>
{
    Task<bool> ExistById(int id);
}