using GamesLib.BusinessLogic.Dtos;
using GamesLib.BusinessLogic.Wrappers.Result;
using GamesLib.DataAccess.Repositories;

namespace GamesLib.BusinessLogic.Handlers.Queries.Publisher
{
    public class GetAllPublishersQuery : IRequestResult<IEnumerable<PublisherDto>>
    {
    }

    public class GetAllPublishersQueryHandler : IRequestHandlerResult<GetAllPublishersQuery, IEnumerable<PublisherDto>>
    {
        private readonly IPublisherRepository _publisherRepository;

        public GetAllPublishersQueryHandler(IPublisherRepository publisherRepository)
        {
            _publisherRepository = publisherRepository ?? throw new ArgumentNullException(nameof(publisherRepository));
        }

        public async Task<Result<IEnumerable<PublisherDto>>> Handle(GetAllPublishersQuery request, CancellationToken cancellationToken)
        {
            var data = (await _publisherRepository.GetAsync())
                .Select(publisher => new PublisherDto
                {
                    Id = publisher.Id,
                    Title = publisher.Title,
                    Description = publisher.Description,
                });

            return Result.Success(data);
        }
    }
}