using System.Diagnostics.CodeAnalysis;

namespace OutHouse.Server.Models
{
    public class Result<TData>
    {

        protected Result(bool isSuccess, TData? data, Error error)
        {
            if (isSuccess && error != Error.None ||
                !isSuccess && error == Error.None)
            {
                throw new ArgumentException("Invalid error", nameof(error));
            }

            IsSuccess = isSuccess;
            Data = data;
            Error = error;
        }

        public TData? Data { get; }

        public bool IsSuccess { get; }

        public bool IsFailure => !IsSuccess;

        public Error Error { get; }

        public static Result<TData> Success(TData? data = default) => new(true, data, Error.None);

        public static Result<TData> Failure(Error error, TData? data = default) => new(false, data, error);
    }

    public class Result(bool isSuccess, Error error) : Result<int?>(isSuccess, null, error)
    {
        public static Result Success() => new(true, Error.None);

        public static Result Failure(Error error) => new(false, error);
    }

    public static class OuthouseErrors
    {
        public static Error InsufficientPrivileges => new(nameof(InsufficientPrivileges), "Requesting user doesn't have privileges for this action");
        public static Error CantModifyLastAdmin => new(nameof(CantModifyLastAdmin), "Can't demote or delete the last admin");
        public static Error MemberAlreadyExists(IUser user) => new(nameof(MemberAlreadyExists), $"User with Id {user.Id} is already a member");
        public static Error MemberNotFound(IUser user) => new(nameof(MemberNotFound), $"User with Id {user.Id} is not a member");
    }

    public sealed record Error(string Code, string Description) : IEqualityComparer<Error>
    {
        public static readonly Error None = new(string.Empty, string.Empty);

        public bool Equals(Error? x, Error? y)
        {
            return x?.Code == y?.Code;
        }

        public int GetHashCode([DisallowNull] Error obj)
        {
            return Code.GetHashCode();
        }
    }
}
