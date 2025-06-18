using Application.Abstracts.Repositories.Cities;
using MediatR;

namespace Application.Features.Cities.Commands.Delete
{
    public class DeleteCityCommandHandler : IRequestHandler<DeleteCityCommandRequest, bool>
    {
        readonly ICityWriteRepository _writeRepository;
        readonly ICityReadRepository _readRepository;

        public DeleteCityCommandHandler(ICityWriteRepository writeRepository, ICityReadRepository readRepository)
        {
            _writeRepository = writeRepository;
            _readRepository = readRepository;
        }

        public async Task<bool> Handle(DeleteCityCommandRequest request, CancellationToken cancellationToken)
        {
            var city = await _readRepository.GetByIdAsync(request.Id);
            if(city == null) 
                return false;

             _writeRepository.Remove(city);
            return true;
        }
    }
}
