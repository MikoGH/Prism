using Moq;
using Prism.Core.Domain.Contracts;
using Prism.Core.Domain.Models;
using Prism.Core.Domain.Models.Includes;
using Prism.Core.Tests.Helpers;
using Prism.Core.WebApi.Exceptions;
using Prism.Core.WebApi.Validators;
using System.Linq.Expressions;

namespace Prism.Core.Tests.Validators;

public class ThemeValidatorTests
{
    [Theory(DisplayName = "HasChanges returns expected result when changed.")]
    [InlineData(sbyte.MaxValue)]
    [InlineData(byte.MaxValue)]
    [InlineData(short.MaxValue)]
    [InlineData(ushort.MaxValue)]
    public async Task HasChanges_ReturnsExpectedResult_WhenChanged(int seed)
    {
        var sporadic = new Random(seed);

        // Arrange
        var themeVersionRepositoryMock = new Mock<IThemeVersionRepository>();

        var themeVersion = ModelGenerator.GenerateThemeVersion(sporadic);
        var lastThemeVersion = ModelGenerator.GenerateThemeVersion(sporadic);

        themeVersionRepositoryMock.Setup(x => x.FirstOrDefaultActualAsync(
            It.IsAny<Expression<Func<ThemeVersion, bool>>>(),
            It.IsAny<ThemeVersionInclude>(),
            It.IsAny<CancellationToken>())
        ).ReturnsAsync(lastThemeVersion);

        var themeValidator = new ThemeValidator(themeVersionRepositoryMock.Object);

        // Act
        var result = await themeValidator.HasChanges(themeVersion, CancellationToken.None);

        // Assert
        Assert.True(result);
    }

    [Theory(DisplayName = "HasChanges returns expected result when did not changed.")]
    [InlineData(sbyte.MaxValue)]
    [InlineData(byte.MaxValue)]
    [InlineData(short.MaxValue)]
    [InlineData(ushort.MaxValue)]
    public async Task HasChanges_ReturnsExpectedResult_WhenNotChanged(int seed)
    {
        var sporadic = new Random(seed);

        // Arrange
        var themeVersionRepositoryMock = new Mock<IThemeVersionRepository>();

        var themeVersion = ModelGenerator.GenerateThemeVersion(sporadic);
        var lastThemeVersion = ModelGenerator.GenerateThemeVersion(sporadic);

        themeVersion.ThemeId = lastThemeVersion.ThemeId;
        themeVersion.Name = lastThemeVersion.Name;
        themeVersion.UserCreateId = lastThemeVersion.UserCreateId;
        themeVersion.IsAccepted = lastThemeVersion.IsAccepted;
        themeVersion.IsDeleted = lastThemeVersion.IsDeleted;

        themeVersionRepositoryMock.Setup(x => x.FirstOrDefaultActualAsync(
            It.IsAny<Expression<Func<ThemeVersion, bool>>>(),
            It.IsAny<ThemeVersionInclude>(),
            It.IsAny<CancellationToken>())
        ).ReturnsAsync(lastThemeVersion);

        var themeValidator = new ThemeValidator(themeVersionRepositoryMock.Object);

        // Act
        var result = await themeValidator.HasChanges(themeVersion, CancellationToken.None);

        // Assert
        Assert.False(result);
    }

    [Theory(DisplayName = "HasChanges returns expected result when is accepted was changed to true.")]
    [InlineData(sbyte.MaxValue)]
    [InlineData(byte.MaxValue)]
    [InlineData(short.MaxValue)]
    [InlineData(ushort.MaxValue)]
    public async Task HasChanges_ReturnsExpectedResult_WhenIsAcceptedChangedToTrue(int seed)
    {
        var sporadic = new Random(seed);

        // Arrange
        var themeVersionRepositoryMock = new Mock<IThemeVersionRepository>();

        var themeVersion = ModelGenerator.GenerateThemeVersion(sporadic);
        var lastThemeVersion = ModelGenerator.GenerateThemeVersion(sporadic);

        themeVersion.ThemeId = lastThemeVersion.ThemeId;
        themeVersion.Name = lastThemeVersion.Name;
        themeVersion.UserCreateId = lastThemeVersion.UserCreateId;
        themeVersion.IsDeleted = lastThemeVersion.IsDeleted;
        lastThemeVersion.IsAccepted = false;
        themeVersion.IsAccepted = true;

        themeVersionRepositoryMock.Setup(x => x.FirstOrDefaultActualAsync(
            It.IsAny<Expression<Func<ThemeVersion, bool>>>(),
            It.IsAny<ThemeVersionInclude>(),
            It.IsAny<CancellationToken>())
        ).ReturnsAsync(lastThemeVersion);

        var themeValidator = new ThemeValidator(themeVersionRepositoryMock.Object);

        // Act
        var result = await themeValidator.HasChanges(themeVersion, CancellationToken.None);

        // Assert
        Assert.True(result);
    }

    [Theory(DisplayName = "HasChanges returns expected result when is accepted was changed to false.")]
    [InlineData(sbyte.MaxValue)]
    [InlineData(byte.MaxValue)]
    [InlineData(short.MaxValue)]
    [InlineData(ushort.MaxValue)]
    public async Task HasChanges_ReturnsExpectedResult_WhenIsAcceptedChangedToFalse(int seed)
    {
        var sporadic = new Random(seed);

        // Arrange
        var themeVersionRepositoryMock = new Mock<IThemeVersionRepository>();

        var themeVersion = ModelGenerator.GenerateThemeVersion(sporadic);
        var lastThemeVersion = ModelGenerator.GenerateThemeVersion(sporadic);

        themeVersion.ThemeId = lastThemeVersion.ThemeId;
        themeVersion.Name = lastThemeVersion.Name;
        themeVersion.UserCreateId = lastThemeVersion.UserCreateId;
        themeVersion.IsDeleted = lastThemeVersion.IsDeleted;
        lastThemeVersion.IsAccepted = true;
        themeVersion.IsAccepted = false;

        themeVersionRepositoryMock.Setup(x => x.FirstOrDefaultActualAsync(
            It.IsAny<Expression<Func<ThemeVersion, bool>>>(),
            It.IsAny<ThemeVersionInclude>(),
            It.IsAny<CancellationToken>())
        ).ReturnsAsync(lastThemeVersion);

        var themeValidator = new ThemeValidator(themeVersionRepositoryMock.Object);

        // Act
        var result = await themeValidator.HasChanges(themeVersion, CancellationToken.None);

        // Assert
        Assert.False(result);
    }

    [Theory(DisplayName = "HasChanges returns expected result when last theme version does not exist.")]
    [InlineData(sbyte.MaxValue)]
    [InlineData(byte.MaxValue)]
    [InlineData(short.MaxValue)]
    [InlineData(ushort.MaxValue)]
    public async Task HasChanges_ReturnsExpectedResult_WhenLastThemeVersionDoesNotExist(int seed)
    {
        var sporadic = new Random(seed);

        // Arrange
        var themeVersionRepositoryMock = new Mock<IThemeVersionRepository>();

        var themeVersion = ModelGenerator.GenerateThemeVersion(sporadic);

        themeVersionRepositoryMock.Setup(x => x.FirstOrDefaultActualAsync(
            It.IsAny<Expression<Func<ThemeVersion, bool>>>(),
            It.IsAny<ThemeVersionInclude>(),
            It.IsAny<CancellationToken>())
        ).ReturnsAsync((ThemeVersion?)null);

        var themeValidator = new ThemeValidator(themeVersionRepositoryMock.Object);

        // Act
        var result = await themeValidator.HasChanges(themeVersion, CancellationToken.None);

        // Assert
        Assert.True(result);
    }
}
