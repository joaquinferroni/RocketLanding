namespace Test.Zartis
{
    public class Result<T>
    {
        public bool IsSuccess { get; private set;}
        public string  Message { get; private set; }
        public T Value{get;private set;}

        private Result(string message)
        => this.Message = message;

        private Result(T value)
        => (IsSuccess, Value) = (true, value);

        public static Result<T> CreateSuccess(T value) => new Result<T>(value);
        public static Result<T> CreateFailure(string message) => new Result<T>(message);
    }
}