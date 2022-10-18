using GamesLib.DataAccess.Context;
using GamesLib.DataAccess.Model;

namespace GamesLib.DataAccess.Repositories
{
    public class PublisherRepository : Repository<Publisher>, IPublisherRepository
    {
        public PublisherRepository(GamesLibContext context) : base(context)
        {
        }

        protected override Publisher CreateEntity(int id)
        {
            return new Publisher { Id = id };
        }
    }
}