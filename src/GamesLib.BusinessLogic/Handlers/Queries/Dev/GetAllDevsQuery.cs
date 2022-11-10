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
        private readonly IDevRepository _devRepository;

        public GetAllDevsQueryHandler(IDevRepository devRepository)
        {
            _devRepository = devRepository ?? throw new ArgumentNullException(nameof(devRepository));
        }

        public async Task<Result<IEnumerable<DevDto>>> Handle(GetAllDevsQuery request, CancellationToken cancellationToken)
        {
            var data = (await _devRepository.GetAsync())
                .Select(dev => new DevDto
                {
                    Id = dev.Id,
                    Title = dev.Title,
                    Description = dev.Description,
                });

            return Result.Success(data);
        }
    }
}