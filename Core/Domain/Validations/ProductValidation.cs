using Domain.Entities;
using FluentValidation;

namespace Domain.Validations
{
    public class ProductValidation : AbstractValidator<Product>
    {
        public ProductValidation()
        {
            
        }
    }
}
