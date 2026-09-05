using Prism.Core.Domain.Models;
using Prism.Core.WebApi.Dtos.Theme;
using Riok.Mapperly.Abstractions;

namespace Prism.Core.WebApi.Mappers;

[Mapper]
public partial class ThemeMapper
{
    public Theme ToTheme(UpsertThemeDto themeDto)
    {
        var theme = new Theme();
        if (themeDto.Id is not null)
        {
            theme.Id = (Guid)themeDto.Id;
        }
        return theme;
    }

    public ThemeVersion ToThemeVersion(UpsertThemeVersionDto themeVersionDto)
    {
        var themeVersion = new ThemeVersion()
        {
            UserCreateId = themeVersionDto.UserId,
            Name = themeVersionDto.Name,
            IsDeleted = false,
            IsAccepted = false
        };

        if (themeVersionDto.Theme.Id is not null)
        {
            themeVersion.ThemeId = (Guid)themeVersionDto.Theme.Id;
        }

        if (themeVersionDto.PreviousVersionId is not null && themeVersionDto.PreviousVersionId != Guid.Empty)
        {
            themeVersion.PreviousVersionId = themeVersionDto.PreviousVersionId;
        }


        themeVersion.Theme = ToTheme(themeVersionDto.Theme);

        return themeVersion;
    }

    public ThemeField ToThemeField(UpsertThemeFieldDto themeFieldDto, Guid themeId)
    {
        var themeField = new ThemeField()
        {
            Type = themeFieldDto.Type,
            ThemeId = themeId
        };

        if (themeFieldDto.Id is not null)
        {
            themeField.Id = (Guid)themeFieldDto.Id;
        }

        return themeField;
    }

    public ThemeFieldVersion ToThemeFieldVersion(UpsertThemeFieldVersionDto themeFieldVersionDto, Guid userId, Guid themeId)
    {
        var themeFieldVersion = new ThemeFieldVersion()
        {
            UserCreateId = userId,
            Priority = themeFieldVersionDto.Priority,
            Name = themeFieldVersionDto.Name,
            IsDeleted = false,
            IsAccepted = false
        };

        if (themeFieldVersionDto.ThemeField.Id is not null)
        {
            themeFieldVersion.ThemeFieldId = (Guid)themeFieldVersionDto.ThemeField.Id;
        }

        if (themeFieldVersionDto.PreviousVersionId is not null && themeFieldVersionDto.PreviousVersionId != Guid.Empty)
        {
            themeFieldVersion.PreviousVersionId = themeFieldVersionDto.PreviousVersionId;
        }

        themeFieldVersion.ThemeField = ToThemeField(themeFieldVersionDto.ThemeField, themeId);

        return themeFieldVersion;
    }
}
