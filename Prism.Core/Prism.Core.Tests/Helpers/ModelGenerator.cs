using Prism.Core.Domain.Models;
using Prism.Core.Domain.Models.Enums;

namespace Prism.Core.Tests.Helpers;

public class ModelGenerator
{
    public static User GenerateUser(Random sporadic)
    {
        return new User()
        {
            Id = RandomGenerator.GenerateGuid(sporadic),
            Role = RandomGenerator.GenerateEnumValue<Role>(sporadic).Value,
            Name = RandomGenerator.GenerateString(sporadic),
            PasswordHash = RandomGenerator.GenerateString(sporadic)
        };
    }

    public static Theme GenerateTheme(Random sporadic)
    {
        return new Theme()
        {
            Id = RandomGenerator.GenerateGuid(sporadic)
        };
    }

    public static ThemeVersion GenerateThemeVersion(Random sporadic)
    {
        var isAccepted = RandomGenerator.GenerateBool(sporadic);
        return new ThemeVersion()
        {
            Id = RandomGenerator.GenerateGuid(sporadic),
            ThemeId = RandomGenerator.GenerateGuid(sporadic),
            PreviousVersionId = RandomGenerator.GenerateGuid(sporadic),
            UserCreateId = RandomGenerator.GenerateGuid(sporadic),
            UserAcceptId = RandomGenerator.GenerateGuid(sporadic),
            WriteDate = RandomGenerator.GenerateDate(sporadic, 2025, 2026),
            IsAccepted = isAccepted,
            IsChecked = isAccepted || RandomGenerator.GenerateBool(sporadic),
            Name = RandomGenerator.GenerateString(sporadic),
            IsDeleted = RandomGenerator.GenerateBool(sporadic)
        };
    }

    public static IEnumerable<ThemeVersion> GenerateThemeVersionCollection(Random sporadic, int count)
    {
        var themeVersionCollection = new List<ThemeVersion>(count);

        var nameCollection = RandomGenerator.GenerateCollectionOfUniqueStrings(sporadic, count).ToList();

        for (int i = 0; i < count; i++)
        {
            var themeVersion = GenerateThemeVersion(sporadic);
            themeVersion.Name = nameCollection[i];
            themeVersionCollection.Add(themeVersion);
        }

        return themeVersionCollection;
    }
}
