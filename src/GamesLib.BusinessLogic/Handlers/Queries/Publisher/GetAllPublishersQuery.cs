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
        private readonly IPublisherRepository _productRepository;

        public GetAllPublishersQueryHandler(IPublisherRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        public async Task<Result<IEnumerable<PublisherDto>>> Handle(GetAllPublishersQuery request, CancellationToken cancellationToken)
        {
            var data = (await _productRepository.GetAsync())
                .Select(dev => new PublisherDto
                {
                    Id = dev.Id,
                    Title = dev.Title,
                });

            return Result.Success(data);
        }
    }
}