namespace Prism.Core.WebApi.Models;

public class ValidationResult
{
    public List<string> ErrorMessages { get; } = new();

    public bool HasErrors
    {
        get => ErrorMessages.Any();
    }

    public void AddErrorMessage(string message)
    {
        ErrorMessages.Add(message);
    }
}
