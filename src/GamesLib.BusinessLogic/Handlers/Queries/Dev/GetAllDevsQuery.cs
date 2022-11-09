using GamesLib.BusinessLogic.Dtos;
using GamesLib.BusinessLogic.Wrappers.Result;
using GamesLib.DataAccess.Repositories;

namespace GamesLib.BusinessLogic.Handlers.Queries.Dev
{
    public class GetAllDevsQuery : IRequestResult<IEnumerable<DevDto>>
    {
    }

    public class GetAllDevsQueryHandler : IRequestHandlerResult<GetAllDevsQuery, IEnumerable<DevDto>>
    {
        private readonly IDevRepository _productRepository;

        public GetAllDevsQueryHandler(IDevRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        public async Task<Result<IEnumerable<DevDto>>> Handle(GetAllDevsQuery request, CancellationToken cancellationToken)
        {
            var data = (await _productRepository.GetAsync())
                .Select(dev => new DevDto
                {
                    Id = dev.Id,
                    Title = dev.Title,
                });

            return Result.Success(data);
        }
    }
}