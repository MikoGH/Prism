using Prism.Core.Domain.Models;
using Prism.Core.WebApi.Dtos.Theme;
using Riok.Mapperly.Abstractions;

namespace Prism.Core.WebApi.Mappers;

[Mapper]
public partial class ThemeMapper
{
    public ThemeVersion ToThemeVersion(AddThemeVersionDto themeVersionDto, Guid userId)
    {
        var themeVersion = new ThemeVersion()
        {
            UserCreateId = userId,
            Name = themeVersionDto.Name,
            IsDeleted = false,
            IsAccepted = false
        };

        if (themeVersionDto.ThemeId is not null)
        {
            themeVersion.ThemeId = (Guid)themeVersionDto.ThemeId;
        }
        else
        {
            themeVersion.Theme = new Theme();
        }

        if (themeVersionDto.PreviousVersionId is not null && themeVersionDto.PreviousVersionId != Guid.Empty)
        {
            themeVersion.PreviousVersionId = themeVersionDto.PreviousVersionId;
        }

        return themeVersion;
    }

    public ThemeField ToThemeField(AddThemeFieldDto themeFieldDto, Guid themeId)
    {
        var themeField = new ThemeField()
        {
            Type = themeFieldDto.Type,
            ThemeId = themeId
        };

        return themeField;
    }

    public ThemeFieldVersion ToThemeFieldVersion(AddThemeFieldVersionDto themeFieldVersionDto, Guid userId, Guid themeId)
    {
        var themeFieldVersion = new ThemeFieldVersion()
        {
            UserCreateId = userId,
            Priority = themeFieldVersionDto.Priority,
            Name = themeFieldVersionDto.Name,
            IsDeleted = false,
            IsAccepted = false
        };

        if (themeFieldVersionDto.ThemeFieldId is not null)
        {
            themeFieldVersion.ThemeFieldId = (Guid)themeFieldVersionDto.ThemeFieldId;
        }
        else if (themeFieldVersionDto.ThemeField is not null)
        {
            themeFieldVersion.ThemeField = ToThemeField(themeFieldVersionDto.ThemeField, themeId);
        }

        if (themeFieldVersionDto.PreviousVersionId is not null && themeFieldVersionDto.PreviousVersionId != Guid.Empty)
        {
            themeFieldVersion.PreviousVersionId = themeFieldVersionDto.PreviousVersionId;
        }

        return themeFieldVersion;
    }

    public partial ThemeVersionDto ToThemeVersionDto(ThemeVersionDto themeVersionDto);

    public partial ThemeFieldVersionDto ToThemeFieldVersionDto(ThemeFieldVersionDto themeFieldVersionDto);
}
