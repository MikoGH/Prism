using Prism.Core.Domain.Models;
using Prism.Core.WebApi.Models;

namespace Prism.Core.WebApi.Validators.Contracts;

public interface IThemeValidator
{
    public ValidationResult Validate(ThemeVersion themeVersion);
}
