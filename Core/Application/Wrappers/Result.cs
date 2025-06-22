namespace Application.Wrappers
{
    public class Result<T>
    {
        public bool IsSuccess { get; private set; }
        public string Message { get; private set; }
        public T Data { get; private set; }
        public List<string> Errors { get; private set; }

        private Result(bool isSuccess, T data, string message, List<string> errors)
        {
            IsSuccess = isSuccess;
            Data = data;
            Message = message;
            Errors = errors ?? [];
        }

        public static Result<T> Success(T data, string message = null) 
            => new Result<T>(true, data, message ?? "Operation completed succesfully", null);

        public static Result<T> Failure(string message, List<string> errors = null)
            => new Result<T>(false, default, message ?? "Operation failed", errors);
    }
}
