using Monq.Core.Paging.Models;
using Prism.Core.Domain.Contracts;
using Prism.Core.Domain.Models;
using Prism.Core.Domain.Models.Enums;
using Prism.Core.Domain.Models.Filters;
using Prism.Core.Domain.Models.Includes;
using Prism.Core.WebApi.Exceptions;
using Prism.Core.WebApi.Services.Contracts;
using Prism.Core.WebApi.Validators.Contracts;

namespace Prism.Core.WebApi.Services;

public class ThemeService : IThemeService
{
    private readonly IThemeValidator _themeValidator;
    private readonly IThemeRepository _themeRepository;
    private readonly IThemeVersionRepository _themeVersionRepository;
    private readonly IUserRepository _userRepository;

    public ThemeService(IThemeValidator themeValidator, IThemeRepository themeRepository,
        IThemeVersionRepository themeVersionRepository, IUserRepository userRepository)
    {
        _themeValidator = themeValidator;
        _themeRepository = themeRepository;
        _themeVersionRepository = themeVersionRepository;
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<ThemeVersion>> FilterAsync(ThemeVersionFilter filter, PagingModel paging, ThemeVersionInclude include, CancellationToken token)
    {
        var themeVersions = await _themeVersionRepository.FilterAsync(filter, paging, include, token);

        return themeVersions;
    }

    public async Task<IEnumerable<ThemeVersion>> FilterActualAsync(ThemeVersionFilter filter, PagingModel paging, ThemeVersionInclude include, CancellationToken token)
    {
        var themeVersions = await _themeVersionRepository.FilterActualAsync(filter, paging, include, token);

        return themeVersions;
    }

    public Task<ThemeVersion?> GetByIdAsync(Guid id, ThemeVersionInclude include, CancellationToken token)
    {
        return _themeVersionRepository.GetByIdAsync(id, include, token);
    }

    public async Task<ThemeVersion?> AddAsync(ThemeVersion themeVersion, CancellationToken token)
    {
        var theme = await GetThemeOrThrowAsync(themeVersion, token);
        var user = await GetUserOrThrowAsync(themeVersion.UserCreateId, token);

        themeVersion.WriteDate = DateTime.UtcNow;
        ApplyTheme(themeVersion, theme);
        ApplyUser(themeVersion, user);

        Validate(themeVersion);

        if (!await HasChangesAsync(themeVersion, token))
            return themeVersion;

        return await _themeVersionRepository.AddAsync(themeVersion, token);
    }

    public async Task<ThemeVersion?> DeleteAsync(Guid themeId, Guid userId, CancellationToken token)
    {
        var lastThemeVersion = await GetLastThemeVersionOrThrowAsync(themeId, token);
        var user = await GetUserOrThrowAsync(userId, token);

        var themeVersion = new ThemeVersion
        {
            ThemeId = themeId,
            UserCreateId = userId,
            Name = lastThemeVersion.Name,
            WriteDate = DateTime.UtcNow,
            IsDeleted = true
        };
        ApplyUser(themeVersion, user);

        return await _themeVersionRepository.AddAsync(themeVersion, token);
    }

    public async Task<bool> HasChangesAsync(ThemeVersion themeVersion, CancellationToken token)
    {
        var previousVersionId = themeVersion.PreviousVersionId;
        if (previousVersionId is null || previousVersionId == Guid.Empty)
            return true;

        var lastThemeVersion = await _themeVersionRepository.GetByIdAsync((Guid)previousVersionId, token: token);
        if (lastThemeVersion is null)
            return true;

        // REM: changed by single IsChecked only if new version has true, old version - false
        return themeVersion.IsChecked && !lastThemeVersion.IsChecked
            || themeVersion.IsChecked && themeVersion.IsAccepted != lastThemeVersion.IsAccepted
            || themeVersion.Name != lastThemeVersion.Name
            || themeVersion.IsDeleted != lastThemeVersion.IsDeleted;
    }

    private void Validate(ThemeVersion themeVersion)
    {
        var validationResult = _themeValidator.Validate(themeVersion);
        if (validationResult.HasErrors)
            throw new ModelValidationException(validationResult);
    }

    private async ValueTask<Theme> GetThemeOrThrowAsync(ThemeVersion themeVersion, CancellationToken token)
    {
        var existingTheme = await GetExistingThemeAsync(themeVersion, token);

        if (existingTheme is not null)
            return existingTheme;

        if (themeVersion.Theme is not null)
            return themeVersion.Theme;

        throw new EntityNotFoundException("Theme does not exist and was not given to add.");
    }

    private async ValueTask<Theme?> GetExistingThemeAsync(ThemeVersion themeVersion, CancellationToken token)
    {
        if (themeVersion.ThemeId != Guid.Empty)
            return await _themeRepository.GetByIdAsync(themeVersion.ThemeId, token);

        if (themeVersion.Theme is not null && themeVersion.Theme.Id != Guid.Empty)
            return await _themeRepository.GetByIdAsync(themeVersion.Theme.Id, token);

        return null;
    }

    private async Task<ThemeVersion> GetLastThemeVersionOrThrowAsync(Guid themeId, CancellationToken token)
    {
        return await _themeVersionRepository.FirstOrDefaultActualAsync(x => x.ThemeId == themeId, token: token)
            ?? throw new EntityNotFoundException("Theme version does not exist.");
    }

    private async Task<User> GetUserOrThrowAsync(Guid userId, CancellationToken token)
    {
        return await _userRepository.GetByIdAsync(userId, token)
            ?? throw new EntityNotFoundException("User does not exist.");
    }

    private static void ApplyTheme(ThemeVersion themeVersion, Theme theme)
    {
        themeVersion.ThemeId = theme.Id;
        themeVersion.Theme = theme;
    }

    private static void ApplyUser(ThemeVersion themeVersion, User user)
    {
        if (IsModeratorOrAdmin(user))
        {
            themeVersion.IsAccepted = true;
            themeVersion.IsChecked = true;
            themeVersion.UserAcceptId = user.Id;
        }
        else
        {
            themeVersion.IsAccepted = false;
            themeVersion.IsChecked = false;
            themeVersion.UserAcceptId = null;
        }
    }

    private static bool IsModeratorOrAdmin(User user)
        => user.Role is Role.Moderator or Role.Admin;
}
