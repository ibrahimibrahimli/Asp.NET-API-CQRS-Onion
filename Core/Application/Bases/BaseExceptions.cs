namespace Application.Bases
{
    public class BaseExceptions : ApplicationException
    {
        public BaseExceptions(){}

        public BaseExceptions(string message) : base(message){}
    }
}
