using System.Net;

namespace ECommerceSystem.Shared.Base
{
    public class Result
    {
        public bool IsSuccess { get; protected set; }
        public bool IsFailure => !IsSuccess;
        public string Error { get; protected set; }
        public HttpStatusCode StatusCode { get; protected set; }

        protected Result(bool isSuccess, string error, HttpStatusCode statusCode)
        {
            IsSuccess = isSuccess;
            Error = error;
            StatusCode = statusCode;
        }

        public static Result NoContent() => new Result(true, string.Empty, HttpStatusCode.NoContent);
        public static Result NotFound(string error, HttpStatusCode statusCode = HttpStatusCode.NotFound)
            => new Result(false, error, statusCode);
        public static Result Success() => new Result(true, string.Empty, HttpStatusCode.OK);
        public static Result Failure(string error, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
            => new Result(false, error, statusCode);
    }

    public class Result<T> : Result
    {
        public T Value { get; private set; }

        protected Result(bool isSuccess, T value, string error, HttpStatusCode statusCode)
            : base(isSuccess, error, statusCode)
        {
            Value = value;
        }

        public static Result<T> Success(T value) => new Result<T>(true, value, string.Empty, HttpStatusCode.OK);
        public static Result<T> NotFound(string error, HttpStatusCode statusCode = HttpStatusCode.NotFound)
            => new Result<T>(false, default, error, statusCode);
        public static Result<T> Failure(string error, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
            => new Result<T>(false, default, error, statusCode);

    }

}
