using Domain.Entities;

namespace Application.Features.Cities.Dtos
{
    public class CityDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PostalCode { get; private set; }
        public Guid CountryId { get; private set; }
        public virtual Country Country { get; private set; }
        public ICollection<Gym> Gyms { get; private set; }
    }
}
