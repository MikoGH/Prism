using Prism.Core.Domain.Contracts;
using Prism.Core.Domain.Models;
using Prism.Core.WebApi.Validators.Contracts;

namespace Prism.Core.WebApi.Validators;

public class ThemeValidator : IThemeValidator
{
    private readonly IThemeVersionRepository _themeVersionRepository;

    public ThemeValidator(IThemeVersionRepository themeVersionRepository)
    {
        _themeVersionRepository = themeVersionRepository;
    }

    public async Task<bool> HasChanges(ThemeVersion themeVersion, CancellationToken token)
    {
        var lastThemeVersion = await _themeVersionRepository.FirstOrDefaultActualAsync(x => x.ThemeId == themeVersion.ThemeId, token: token);
        if (lastThemeVersion is null)
            throw new Exception("Theme version does not exist.");  // TODO: add custom exception

        // REM: changed by single IsAccepted only if new version has true, old version - false
        if ((themeVersion.IsAccepted || !lastThemeVersion.IsAccepted)
            && themeVersion.Name == lastThemeVersion.Name
            && themeVersion.IsDeleted == lastThemeVersion.IsDeleted)
        {
            return false;
        }

        return true;
    }
}
