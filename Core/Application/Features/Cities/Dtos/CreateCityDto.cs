namespace Application.Features.Cities.Dtos
{
    public class CreateCityDto
    {
        public string Name { get; set; } = default!;
        public string PostalCode { get; set; } = default!;
        public Guid CountryId { get; set; }
    }
}
