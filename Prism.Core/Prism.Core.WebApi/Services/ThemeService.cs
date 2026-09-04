using Monq.Core.Paging.Models;
using Prism.Core.Domain.Contracts;
using Prism.Core.Domain.Models;
using Prism.Core.Domain.Models.Enums;
using Prism.Core.Domain.Models.Filters;
using Prism.Core.Domain.Models.Includes;
using Prism.Core.WebApi.Exceptions;
using Prism.Core.WebApi.Services.Contracts;
using Prism.Core.WebApi.Validators.Contracts;
using System.ComponentModel.DataAnnotations;

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

    public async Task<IEnumerable<ThemeVersion>> FilterAsync(PagingModel paging, ThemeVersionFilter filter, CancellationToken token)
    {
        var include = new ThemeVersionInclude
        {
            Theme = true
        };
        var themeVersions = await _themeVersionRepository.FilterAsync(filter, paging, include, token);

        return themeVersions;
    }

    public async Task<IEnumerable<ThemeVersion>> FilterActualAsync(PagingModel paging, ThemeVersionFilter filter, CancellationToken token)
    {
        var include = new ThemeVersionInclude
        {
            Theme = true
        };
        var themeVersions = await _themeVersionRepository.FilterActualAsync(filter, paging, include, token);

        return themeVersions;
    }

    public Task<ThemeVersion?> GetByIdAsync(Guid id, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public async Task<ThemeVersion?> AddAsync(ThemeVersion themeVersion, CancellationToken token)
    {
        var theme = await GetOrAddThemeAsync(themeVersion, token);
        if (theme is null)
            throw new EntityNotFoundException("Theme does not exist and was not given to add.");

        themeVersion.ThemeId = theme.Id;
        themeVersion.WriteDate = DateTime.UtcNow;

        var user = await _userRepository.GetByIdAsync(themeVersion.UserCreateId, token);
        if (user is null)
            throw new EntityNotFoundException("User does not exist.");

        if (user.Role is Role.Moderator or Role.Admin)
        {
            themeVersion.IsAccepted = true;
            themeVersion.UserAcceptId = user.Id;
        }

        var hasChanges = await _themeValidator.HasChanges(themeVersion, token);
        if (!hasChanges)
            return themeVersion;

        var addedThemeVersion = await _themeVersionRepository.AddAsync(themeVersion, token);

        return addedThemeVersion;
    }

    public async Task<ThemeVersion?> DeleteAsync(Guid themeId, Guid userId, CancellationToken token)
    {
        var lastThemeVersion = await _themeVersionRepository.FirstOrDefaultActualAsync(x => x.ThemeId == themeId, token: token);
        if (lastThemeVersion is null)
            throw new EntityNotFoundException("Theme version does not exist.");

        var themeVersion = new ThemeVersion
        {
            ThemeId = themeId,
            UserCreateId = userId,
            Name = lastThemeVersion.Name,
            WriteDate = DateTime.UtcNow,
            IsDeleted = true
        };

        var user = await _userRepository.GetByIdAsync(userId, token);
        if (user is null)
            throw new EntityNotFoundException("User does not exist.");

        if (user.Role is Role.Moderator or Role.Admin)
        {
            themeVersion.IsAccepted = true;
            themeVersion.UserAcceptId = user.Id;
        }

        return await _themeVersionRepository.AddAsync(themeVersion, token);
    }

    private async ValueTask<Theme?> GetOrAddThemeAsync(ThemeVersion themeVersion, CancellationToken token)
    {
        if (themeVersion.ThemeId != Guid.Empty)
            return await _themeRepository.GetByIdAsync(themeVersion.ThemeId, token);

        if (themeVersion.Theme is not null)
        {
            if (themeVersion.Theme.Id != Guid.Empty)
                return await _themeRepository.GetByIdAsync(themeVersion.Theme.Id, token);

            return await _themeRepository.AddAsync(themeVersion.Theme, token);
        }

        return null;
    }
}
