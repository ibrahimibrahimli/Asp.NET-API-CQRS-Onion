using Application.Abstracts.Repositories.Cities;
using Application.Features.Cities.Dtos;
using MediatR;

namespace Application.Features.Cities.Queries
{
    public class GetAllCitiesQueryHandler : IRequestHandler<GetAllCitiesQueryRequest, List<CityDto>>
    {
        readonly ICityReadRepository _readRepository;

        public GetAllCitiesQueryHandler(ICityReadRepository readRepository)
        {
            _readRepository = readRepository;
        }

        public async Task<List<CityDto>> Handle(GetAllCitiesQueryRequest request, CancellationToken cancellationToken)
        {
            var cities = await _readRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return new();
        }
    }
}
