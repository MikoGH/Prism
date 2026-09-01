using System.ComponentModel.DataAnnotations;

namespace Prism.Core.WebApi.Exceptions;

public class AuthException(string message) : ValidationException(message)
{
}
