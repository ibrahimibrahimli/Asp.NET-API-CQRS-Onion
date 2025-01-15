using Application.Abstracts.RedisCache;
using MediatR;

namespace Application.Features.Brands.Queries
{
    public class GetAllBrandsQueryRequest : IRequest<IList<GetAllBrandsQueryResponse>>, ICacheableQuery
    {
        public string CacheKey => "GetAllBrands";

        public double CacheTime => 5;
    }
}
