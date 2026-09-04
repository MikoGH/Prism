using Prism.Core.Tests.Helpers;
using Prism.Core.WebApi.Validators;

namespace Prism.Core.Tests.Validators;

public class ThemeValidatorTests
{
    [Theory(DisplayName = "Validate returns expected result when ThemeId and Theme.Id are distinct.")]
    [InlineData(sbyte.MaxValue)]
    [InlineData(byte.MaxValue)]
    [InlineData(short.MaxValue)]
    [InlineData(ushort.MaxValue)]
    public async Task Validate_ReturnsExpectedResult_WhenThemeIdDistinct(int seed)
    {
        var sporadic = new Random(seed);

        // Arrange
        var theme = ModelGenerator.GenerateTheme(sporadic);
        var themeVersion = ModelGenerator.GenerateThemeVersion(sporadic);
        themeVersion.Theme = theme;

        var themeValidator = new ThemeValidator();

        // Act
        var result = themeValidator.Validate(themeVersion);

        // Assert
        Assert.True(result.HasErrors);
    }

    [Theory(DisplayName = "Validate returns expected result when ThemeId and Theme.Id are same.")]
    [InlineData(sbyte.MaxValue)]
    [InlineData(byte.MaxValue)]
    [InlineData(short.MaxValue)]
    [InlineData(ushort.MaxValue)]
    public async Task Validate_ReturnsExpectedResult_WhenThemeIdSame(int seed)
    {
        var sporadic = new Random(seed);

        // Arrange
        var theme = ModelGenerator.GenerateTheme(sporadic);
        var themeVersion = ModelGenerator.GenerateThemeVersion(sporadic);
        themeVersion.Theme = theme;
        themeVersion.ThemeId = theme.Id;

        var themeValidator = new ThemeValidator();

        // Act
        var result = themeValidator.Validate(themeVersion);

        // Assert
        Assert.False(result.HasErrors);
    }
}
