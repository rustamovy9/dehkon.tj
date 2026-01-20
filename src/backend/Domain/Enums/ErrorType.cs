namespace Domain.Enums;

public enum ErrorType
{
    None,
    BadRequest,
    NotFound,
    Forbidden,
    AlreadyExist,
    Conflict,
    InternalServerError
}