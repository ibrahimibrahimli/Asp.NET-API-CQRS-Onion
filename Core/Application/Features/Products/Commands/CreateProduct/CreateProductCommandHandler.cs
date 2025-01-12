using Application.Abstracts.AutoMapper;
using Application.Abstracts.UoW;
using Application.Bases;
using Application.Features.Products.Rules;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : BaseHandler, IRequestHandler<CreateProductCommandRequest, Unit>
    {
        private readonly ProductRules _productRules;

        public CreateProductCommandHandler(ProductRules productRules, IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) 
            : base(mapper, unitOfWork, httpContextAccessor)
        {
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
