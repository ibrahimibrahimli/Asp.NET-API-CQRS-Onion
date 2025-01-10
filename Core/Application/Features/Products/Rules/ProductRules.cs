using Application.Bases;
using Application.Features.Products.Exceptions;
using Domain.Entities;

namespace Application.Features.Products.Rules
{
    public class ProductRules : BaseRules
    {
        public Task ProductTitleNotMustBeSame(IList<Product> products, string requestTitle)
        {
            if(products.Any(p => p.Title == requestTitle)) throw new ProductTitleNotMustBeSameException();

            return Task.CompletedTask;
        }
    }
}
