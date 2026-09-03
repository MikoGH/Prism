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

        themeVersion.Theme = ToTheme(themeVersionDto.Theme);

        return themeVersion;
    }

    public ThemeField ToThemeField(UpsertThemeVersionDto themeVersionDto, UpsertThemeFieldDto themeFieldDto)
    {
        var themeField = new ThemeField()
        {
            Type = themeFieldDto.Type
        };

        if (themeVersionDto.Theme.Id is not null)
        {
            themeField.ThemeId = (Guid)themeVersionDto.Theme.Id;
        }

        if (themeFieldDto.Id is not null)
        {
            themeField.Id = (Guid)themeFieldDto.Id;
        }

        return themeField;
    }

    public ThemeFieldVersion ToThemeFieldVersion(UpsertThemeVersionDto themeVersionDto, UpsertThemeFieldVersionDto themeFieldVersionDto)
    {
        var themeFieldVersion = new ThemeFieldVersion()
        {
            UserCreateId = themeVersionDto.UserId,
            Priority = themeFieldVersionDto.Priority,
            Name = themeFieldVersionDto.Name,
            IsDeleted = false,
            IsAccepted = false
        };

        if (themeFieldVersionDto.ThemeField.Id is not null)
        {
            themeFieldVersion.ThemeFieldId = (Guid)themeFieldVersionDto.ThemeField.Id;
        }

        themeFieldVersion.ThemeField = ToThemeField(themeVersionDto, themeFieldVersionDto.ThemeField);

        return themeFieldVersion;
    }
}
