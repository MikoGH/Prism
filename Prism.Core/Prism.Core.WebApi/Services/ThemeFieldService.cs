using Flequery.Extensions;
using Flequery.Models;
using Flequery.Models.Enums;
using Prism.Core.Domain.Contracts;
using Prism.Core.Domain.Models;
using Prism.Core.Domain.Models.Enums;
using Prism.Core.WebApi.Exceptions;
using Prism.Core.WebApi.Services.Contracts;
using Prism.Core.WebApi.Validators.Contracts;

namespace Prism.Core.WebApi.Services;

public class ThemeFieldService : IThemeFieldService
{
    private readonly IThemeFieldValidator _themeFieldValidator;
    private readonly IThemeFieldRepository _themeFieldRepository;
    private readonly IThemeFieldVersionRepository _themeFieldVersionRepository;
    private readonly IUserRepository _userRepository;

    public ThemeFieldService(IThemeFieldValidator themeFieldValidator, IThemeFieldRepository themeFieldRepository,
        IThemeFieldVersionRepository themeFieldVersionRepository, IUserRepository userRepository)
    {
        _themeFieldValidator = themeFieldValidator;
        _themeFieldRepository = themeFieldRepository;
        _themeFieldVersionRepository = themeFieldVersionRepository;
        _userRepository = userRepository;
    }

    public async Task<PagedResponse<ThemeFieldVersion>> FilterAsync(QueryRequest request, CancellationToken token)
    {
        var themeFieldVersions = await _themeFieldVersionRepository.FilterAsync(request, token);

        return themeFieldVersions;
    }

    public async Task<PagedResponse<ThemeFieldVersion>> FilterActualAsync(QueryRequest request, CancellationToken token)
    {
        var themeFieldVersions = await _themeFieldVersionRepository.FilterActualAsync(request, token);

        return themeFieldVersions;
    }

    public Task<ThemeFieldVersion?> GetByIdAsync(Guid id, IncludeRequest include, CancellationToken token)
    {
        return _themeFieldVersionRepository.GetByIdAsync(id, include, token);
    }

    public async Task BatchAsync(IEnumerable<ThemeFieldVersion> themeFieldVersions, Guid themeId, CancellationToken token)
    {
        var existingThemeFields = await GetExistingThemeFieldsAsync(themeFieldVersions, token);
        var userId = themeFieldVersions.Select(x => x.UserCreateId).Single();
        var user = await GetUserOrThrowAsync(userId, token);

        ApplyExistingThemeFields(themeFieldVersions, existingThemeFields);
        ApplyUser(themeFieldVersions, user);

        var changedThemeFieldVersions = await FilterChangedAsync(themeFieldVersions, token);

        Validate(changedThemeFieldVersions, themeId);

        await _themeFieldVersionRepository.BatchAsync(changedThemeFieldVersions, token);
    }

    public async Task<IEnumerable<ThemeFieldVersion>> FilterChangedAsync(IEnumerable<ThemeFieldVersion> themeFieldVersions, CancellationToken token)
    {
        var newThemeFieldVersions = themeFieldVersions
            .Where(x => x.PreviousVersionId is null)
            .ToList();

        var existingThemeFieldVersions = themeFieldVersions
            .Except(newThemeFieldVersions)
            .ToList();

        var filterIds = themeFieldVersions
            .Except(newThemeFieldVersions)
            .Select(x => (Guid)x.PreviousVersionId!)
            .ToList();
        var request = new QueryRequest();
        request.AddFilter<ThemeFieldVersion, Guid>(x => x.Id, FilterOperator.In, filterIds);
        var lastThemeFieldVersions = await _themeFieldVersionRepository.FilterAsync(request, token: token);
        var lastThemeFieldVersionDct = lastThemeFieldVersions.Records.ToDictionary(x => x.Id, x => x);

        if (lastThemeFieldVersions.Records.Count() != existingThemeFieldVersions.Count)
            throw new EntityNotFoundException("ThemeFieldVersion.PreviousVersionId does not exist.");

        var changedThemeFieldVersions = existingThemeFieldVersions
            .Where(x => HasChanges(x, lastThemeFieldVersionDct[(Guid)x.PreviousVersionId!]))
            .Concat(newThemeFieldVersions)
            .ToList();

        return changedThemeFieldVersions;
    }

    public bool HasChanges(ThemeFieldVersion themeFieldVersion, ThemeFieldVersion lastThemeFieldVersion)
    {
        // REM: changed by single IsChecked only if new version has true, old version - false
        return themeFieldVersion.IsChecked && !lastThemeFieldVersion.IsChecked
            || themeFieldVersion.IsChecked && themeFieldVersion.IsAccepted != lastThemeFieldVersion.IsAccepted
            || themeFieldVersion.Name != lastThemeFieldVersion.Name
            || themeFieldVersion.Priority != lastThemeFieldVersion.Priority
            || themeFieldVersion.IsDeleted != lastThemeFieldVersion.IsDeleted;
    }

    public async Task<bool> HasChangesAsync(ThemeFieldVersion themeFieldVersion, CancellationToken token)
    {
        var previousVersionId = themeFieldVersion.PreviousVersionId;
        if (previousVersionId is null || previousVersionId == Guid.Empty)
            return true;

        var lastThemeFieldVersion = await _themeFieldVersionRepository.GetByIdAsync((Guid)previousVersionId, token: token);
        if (lastThemeFieldVersion is null)
            return true;

        return HasChanges(themeFieldVersion, lastThemeFieldVersion);
    }

    private void Validate(IEnumerable<ThemeFieldVersion> themeFieldVersions, Guid themeId)
    {
        if (!CheckThemeFieldDistinct(themeFieldVersions))
            throw new Exception("Received same theme field ids.");

        if (!CheckThemeIdSame(themeFieldVersions, themeId))
            throw new Exception("Received distinct theme ids.");

        var validationResults = themeFieldVersions.Select(x => _themeFieldValidator.Validate(x)).ToList();
        if (validationResults.Count != 0)
        {
            var errorMessages = validationResults.SelectMany(x => x.ErrorMessages).ToList();
            throw new ModelValidationException(errorMessages);
        }
    }

    private static bool CheckThemeFieldDistinct(IEnumerable<ThemeFieldVersion> themeFieldVersions)
    {
        var themeFieldIds = themeFieldVersions.Select(x => x.ThemeFieldId).ToList();

        return themeFieldIds.Distinct().Count() == themeFieldIds.Count;
    }

    private static bool CheckThemeIdSame(IEnumerable<ThemeFieldVersion> themeFieldVersions, Guid themeId)
    {
        var existingThemeId = themeFieldVersions
            .Where(x => x.ThemeField is not null)
            .Select(x => x.ThemeField!.ThemeId)
            .SingleOrDefault();

        if (existingThemeId == Guid.Empty)
            return true;

        return existingThemeId == themeId;
    }

    private async Task<Dictionary<Guid, ThemeField>> GetExistingThemeFieldsAsync(IEnumerable<ThemeFieldVersion> themeFieldVersions, CancellationToken token)
    {
        var existingThemeFieldIds = themeFieldVersions
            .Select(x => GetThemeFieldId(x))
            .Where(x => x != Guid.Empty)
            .ToList();

        var request = new QueryRequest();
        request.AddFilter<ThemeField, Guid>(x => x.Id, FilterOperator.In, existingThemeFieldIds);
        var existingThemeFields = await _themeFieldRepository.FilterAsync(request, token: token);

        return existingThemeFields.Records.ToDictionary(x => x.Id, x => x);
    }

    private static Guid GetThemeFieldId(ThemeFieldVersion themeFieldVersion)
    {
        if (themeFieldVersion.ThemeFieldId != Guid.Empty)
            return themeFieldVersion.ThemeFieldId;

        if (themeFieldVersion.ThemeField is not null && themeFieldVersion.ThemeField.Id != Guid.Empty)
            return themeFieldVersion.ThemeField.Id;

        return Guid.Empty;
    }

    private async Task<User> GetUserOrThrowAsync(Guid userId, CancellationToken token)
    {
        return await _userRepository.GetByIdAsync(userId, token)
            ?? throw new EntityNotFoundException("User does not exist.");
    }

    private static void ApplyExistingThemeFields(IEnumerable<ThemeFieldVersion> themeFieldVersions, Dictionary<Guid, ThemeField> existingThemeFields)
    {
        foreach (var themeFieldVersion in themeFieldVersions)
        {
            if (themeFieldVersion.ThemeFieldId != Guid.Empty
                && existingThemeFields.TryGetValue(themeFieldVersion.ThemeFieldId, out var themeField))
            {
                themeFieldVersion.ThemeField = themeField;
                themeFieldVersion.ThemeFieldId = themeField.Id;
            }

            if (themeFieldVersion.ThemeField is not null
                && themeFieldVersion.ThemeField.Id != Guid.Empty
                && existingThemeFields.TryGetValue(themeFieldVersion.ThemeField.Id, out themeField))
            {
                themeFieldVersion.ThemeField = themeField;
                themeFieldVersion.ThemeFieldId = themeField.Id;
            }
        }
    }

    private static void ApplyUser(IEnumerable<ThemeFieldVersion> themeFieldVersions, User user)
    {
        foreach (var themeFieldVersion in themeFieldVersions)
        {
            ApplyUser(themeFieldVersion, user);
        }
    }

    private static void ApplyUser(ThemeFieldVersion themeFieldVersion, User user)
    {
        if (IsModeratorOrAdmin(user))
        {
            themeFieldVersion.IsAccepted = true;
            themeFieldVersion.IsChecked = true;
            themeFieldVersion.UserAcceptId = user.Id;
        }
        else
        {
            themeFieldVersion.IsAccepted = false;
            themeFieldVersion.IsChecked = false;
            themeFieldVersion.UserAcceptId = null;
        }
    }

    private static bool IsModeratorOrAdmin(User user)
        => user.Role is Role.Moderator or Role.Admin;
}
