using Application.Features.Cities.Dtos;
using MediatR;

namespace Application.Features.Cities.Queries
{
    public class GetAllCitiesQueryRequest : IRequest<List<CityDto>>
    {
    }
}
