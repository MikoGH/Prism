using Prism.Core.Domain.Models;
using Prism.Core.WebApi.Models;
using Prism.Core.WebApi.Validators.Contracts;

namespace Prism.Core.WebApi.Validators;

public class ThemeFieldValidator : IThemeFieldValidator
{
    public ValidationResult Validate(ThemeFieldVersion themeFieldVersion)
    {
        var validationResult = new ValidationResult();
        if (themeFieldVersion.ThemeField is not null
            && themeFieldVersion.ThemeField.Id != Guid.Empty
            && themeFieldVersion.ThemeFieldId != Guid.Empty
            && themeFieldVersion.ThemeField.Id != themeFieldVersion.ThemeFieldId)
            validationResult.AddErrorMessage("ThemeFieldVersion.ThemeFieldId and ThemeFieldVersion.ThemeField.Id distinct.");

        if ((themeFieldVersion.ThemeFieldId != Guid.Empty) == (themeFieldVersion.PreviousVersionId is null))
            validationResult.AddErrorMessage("ThemeField and PreviousVersion existence distinct.");

        if (!themeFieldVersion.IsChecked && themeFieldVersion.IsAccepted)
            validationResult.AddErrorMessage("ThemeFieldVersion accepted but not checked.");

        if (themeFieldVersion.Name is null || themeFieldVersion.Name == String.Empty)
            validationResult.AddErrorMessage("ThemeFieldVersion.Name is empty string.");

        if (themeFieldVersion.Priority < 0)
            validationResult.AddErrorMessage("Priority is below 0.");

        return validationResult;
    }
}
