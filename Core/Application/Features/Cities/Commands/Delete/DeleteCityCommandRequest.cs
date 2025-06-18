using MediatR;

namespace Application.Features.Cities.Commands.Delete
{
    public class DeleteCityCommandRequest : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
