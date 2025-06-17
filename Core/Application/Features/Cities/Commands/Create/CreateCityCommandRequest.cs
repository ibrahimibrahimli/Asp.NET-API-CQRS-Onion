using Application.Features.Cities.Dtos;
using MediatR;

namespace Application.Features.Cities.Commands.Create
{
    public class CreateCityCommandRequest(CreateCityDto cityDto) : IRequest<Guid>
    {
        public CreateCityDto CityDto { get; set; } = cityDto;
    }
}
