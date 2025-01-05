using Application.UoW;
using MediatR;
using Domain.Entities;

namespace Application.Features.Products.Queries.GetAllProducts
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQueryRequest, IList<GetAllProductsQueryResponse>>
    {
        private readonly IUnitOfWork? _unitOfWork;

        public GetAllProductsQueryHandler(IUnitOfWork? unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IList<GetAllProductsQueryResponse>> Handle(GetAllProductsQueryRequest request, CancellationToken cancellationToken)
        {
            List<GetAllProductsQueryResponse> response = new();
            var products = await _unitOfWork.GetReadRepository<Product>().GetAllAsync();
            foreach (var product in products) 
            {
                response.Add(new GetAllProductsQueryResponse()
                    {
                        Title = product.Title,
                        Description = product.Description,
                        Discount = product.Discount,    
                        Price = product.Price - (product.Price * product.Discount / 100),
                    });
            }
            return response;
        }
    }
}
