using Application.Bases;

namespace Application.Features.Products.Exceptions
{
    public class ProductTitleNotMustBeSameException : BaseException
    {
        public ProductTitleNotMustBeSameException() : base(BaseUiMessages.EXCEPTION_TITLE_NOT_SAME) { }
    }
}
