using Application.Abstracts.AutoMapper;
using Application.Abstracts.UoW;
using Application.Bases;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Brands.Queries
{
    public class GetAllBrandsQueryHandler : BaseHandler, IRequestHandler<GetAllBrandsQueryRequest, IList<GetAllBrandsQueryResponse>>
    {
        public GetAllBrandsQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork, httpContextAccessor)
        {
        }

        async Task<IList<GetAllBrandsQueryResponse>> IRequestHandler<GetAllBrandsQueryRequest, IList<GetAllBrandsQueryResponse>>.Handle(GetAllBrandsQueryRequest request, CancellationToken cancellationToken)
        {
            var brands = await _unitOfWork.GetReadRepository<Brand>().GetAllAsync();

            return _mapper.Map<GetAllBrandsQueryResponse, Brand>(brands);
        }
    }
}
