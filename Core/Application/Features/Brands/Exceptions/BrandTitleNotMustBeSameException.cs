using Application.Bases;

namespace Application.Features.Brands.Exceptions
{
    public class BrandTitleNotMustBeSameException : BaseException
    {
        public BrandTitleNotMustBeSameException() : base(BaseUiMessages.EXCEPTION_TITLE_NOT_SAME) { }
    }
}
