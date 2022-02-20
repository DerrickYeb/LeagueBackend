using System.Net;

namespace Application.Exceptions;

public class ValidationExceptions:CustomException
{
    public ValidationExceptions(List<string> errors = default) : base("Validation errors occured", errors, HttpStatusCode.BadRequest)
    {
    }
}