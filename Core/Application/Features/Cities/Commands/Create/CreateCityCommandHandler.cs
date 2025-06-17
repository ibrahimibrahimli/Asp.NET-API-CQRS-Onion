using Application.Abstracts.Repositories.Cities;
using MediatR;

namespace Application.Features.Cities.Commands.Create
{
    public class CreateCityCommandHandler : IRequestHandler<CreateCityCommandRequest, Guid>
    {
        readonly ICityWriteRepository _writeRepository;

        public CreateCityCommandHandler(ICityWriteRepository writeRepository)
        {
            _writeRepository = writeRepository;
        }

        public async Task<Guid> Handle(CreateCityCommandRequest request, CancellationToken cancellationToken)
        {
            var dto = request.CityDto;

            var city = City.Create(dto.Name, dto.CountryId, dto.PostalCode);
            await _writeRepository.AddAsync(city, cancellationToken);
            await _writeRepository.SaveChangesAsync(cancellationToken);

            return city.Id;
        }
    }
}
