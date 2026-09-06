using Prism.Core.Domain.Models;
using Prism.Core.WebApi.Models;
using Prism.Core.WebApi.Validators.Contracts;

namespace Prism.Core.WebApi.Validators;

public class ThemeValidator : IThemeValidator
{
    public ValidationResult Validate(ThemeVersion themeVersion)
    {
        var validationResult = new ValidationResult();
        if (themeVersion.Theme is not null
            && themeVersion.Theme.Id != Guid.Empty
            && themeVersion.ThemeId != Guid.Empty
            && themeVersion.Theme.Id != themeVersion.ThemeId)
            validationResult.AddErrorMessage("ThemeVersion.ThemeId and ThemeVersion.Theme.Id distinct.");

        if ((themeVersion.ThemeId != Guid.Empty) == (themeVersion.PreviousVersionId is null))
            validationResult.AddErrorMessage("Theme and PreviousVersion existence distinct.");

        if (!themeVersion.IsChecked && themeVersion.IsAccepted)
            validationResult.AddErrorMessage("ThemeVersion accepted but not checked.");

        if (themeVersion.Name is null || themeVersion.Name == String.Empty)
            validationResult.AddErrorMessage("ThemeVersion.Name is empty string.");

        return validationResult;
    }
}
