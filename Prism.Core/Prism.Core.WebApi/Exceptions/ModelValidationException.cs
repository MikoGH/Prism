using System.ComponentModel.DataAnnotations;

namespace Prism.Core.WebApi.Exceptions;

public class ModelValidationException : ValidationException
{
    public ModelValidationException(Models.ValidationResult validationResult)
        : base(string.Join("; ", validationResult.ErrorMessages))
    { }

    public ModelValidationException(List<string> messages)
        : base(string.Join("; ", messages))
    { }

    public ModelValidationException(string message)
        : base(message)
    { }
}
