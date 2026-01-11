using System.Runtime.InteropServices.JavaScript;
using Domain.Common;

namespace Application.Extensions.ResultPattern;

public class Result<T> : BaseResult
{
    public T? Value { get; set; }

    protected Result(bool isSuccess, Error error,T? value) : base(isSuccess, error)
    {
        Value = value;
    }

    public static Result<T> Success(T? value) => new(true, Error.None(), value);
    public static Result<T> Failure(Error error) => new(false, error, default);
}