using Application.Features.Products.Rules;
using Application.UoW;
using Domain.Entities;
using MediatR;

namespace Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ProductRules _productRules;

        public CreateProductCommandHandler(IUnitOfWork unitOfWork, ProductRules productRules)
        {
            _unitOfWork = unitOfWork;
            _productRules = productRules;
        }
        public async Task<Unit> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            List<Product> products = await _unitOfWork.GetReadRepository<Product>().GetAllAsync();
            await _productRules.ProductTitleNotMustBeSame(products, request.Title);

            Product product = new(request.Title, request.Description, request.Price, request.BrandId, request.Discount);

            await _unitOfWork.GetWriteRepository<Product>().AddAsync(product);
            if (await _unitOfWork.SaveAsync() > 0)
            {
                foreach (var categoryId in request.CategoryIds)
                    await _unitOfWork.GetWriteRepository<ProductCategory>().AddAsync(new()
                    {
                        ProductId = product.Id,
                        CategoryId = categoryId,
                    });

                await _unitOfWork.SaveAsync();
            }
            return Unit.Value;
        }
    }
}
