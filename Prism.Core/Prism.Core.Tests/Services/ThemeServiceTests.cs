using Moq;
using Prism.Core.Domain.Contracts;
using Prism.Core.Domain.Models;
using Prism.Core.Tests.Helpers;
using Prism.Core.WebApi.Exceptions;
using Prism.Core.WebApi.Services;
using Prism.Core.WebApi.Validators.Contracts;

namespace Prism.Core.Tests.Services;

public class ThemeServiceTests
{
    [Theory(DisplayName = "AddAsync returns expected result when theme id given.")]
    [InlineData(sbyte.MaxValue)]
    [InlineData(byte.MaxValue)]
    [InlineData(short.MaxValue)]
    [InlineData(ushort.MaxValue)]
    public async Task AddAsync_ReturnsExpectedResult_WhenThemeIdGiven(int seed)
    {
        var sporadic = new Random(seed);

        // Arrange
        var themeValidatorMock = new Mock<IThemeValidator>();
        var themeRepositoryMock = new Mock<IThemeRepository>();
        var themeVersionRepositoryMock = new Mock<IThemeVersionRepository>();
        var userRepositoryMock = new Mock<IUserRepository>();

        var theme = ModelGenerator.GenerateTheme(sporadic);
        var themeVersion = ModelGenerator.GenerateThemeVersion(sporadic);
        var user = ModelGenerator.GenerateUser(sporadic);

        themeRepositoryMock.Setup(x => x.GetByIdAsync(
            It.IsAny<Guid>(),
            It.IsAny<CancellationToken>())
        ).ReturnsAsync(theme);

        userRepositoryMock.Setup(x => x.GetByIdAsync(
            It.IsAny<Guid>(),
            It.IsAny<CancellationToken>())
        ).ReturnsAsync(user);

        themeValidatorMock.Setup(x => x.HasChanges(
            It.IsAny<ThemeVersion>(),
            It.IsAny<CancellationToken>())
        ).ReturnsAsync(true);

        var themeService = new ThemeService(themeValidatorMock.Object, themeRepositoryMock.Object, themeVersionRepositoryMock.Object, userRepositoryMock.Object);

        // Act
        var result = await themeService.AddAsync(themeVersion, CancellationToken.None);

        // Assert
        themeVersionRepositoryMock.Verify(
            x => x.AddAsync(
                It.IsAny<ThemeVersion>(),
                It.IsAny<CancellationToken>()),
            Times.Once);

        themeRepositoryMock.Verify(
            x => x.AddAsync(
                It.IsAny<Theme>(),
                It.IsAny<CancellationToken>()),
            Times.Never);

        themeRepositoryMock.Verify(
            x => x.GetByIdAsync(
                It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Theory(DisplayName = "AddAsync returns expected result when theme was given to add.")]
    [InlineData(sbyte.MaxValue)]
    [InlineData(byte.MaxValue)]
    [InlineData(short.MaxValue)]
    [InlineData(ushort.MaxValue)]
    public async Task AddAsync_ReturnsExpectedResult_WhenThemeAddRequired(int seed)
    {
        var sporadic = new Random(seed);

        // Arrange
        var themeValidatorMock = new Mock<IThemeValidator>();
        var themeRepositoryMock = new Mock<IThemeRepository>();
        var themeVersionRepositoryMock = new Mock<IThemeVersionRepository>();
        var userRepositoryMock = new Mock<IUserRepository>();

        var theme = ModelGenerator.GenerateTheme(sporadic);
        var themeVersion = ModelGenerator.GenerateThemeVersion(sporadic);
        theme.Id = default;
        themeVersion.Theme = theme;
        themeVersion.ThemeId = default;
        var user = ModelGenerator.GenerateUser(sporadic);

        themeRepositoryMock.Setup(x => x.AddAsync(
            It.IsAny<Theme>(),
            It.IsAny<CancellationToken>())
        ).ReturnsAsync(theme);

        userRepositoryMock.Setup(x => x.GetByIdAsync(
            It.IsAny<Guid>(),
            It.IsAny<CancellationToken>())
        ).ReturnsAsync(user);

        themeValidatorMock.Setup(x => x.HasChanges(
            It.IsAny<ThemeVersion>(),
            It.IsAny<CancellationToken>())
        ).ReturnsAsync(true);

        var themeService = new ThemeService(themeValidatorMock.Object, themeRepositoryMock.Object, themeVersionRepositoryMock.Object, userRepositoryMock.Object);

        // Act
        var result = await themeService.AddAsync(themeVersion, CancellationToken.None);

        // Assert
        themeVersionRepositoryMock.Verify(
            x => x.AddAsync(
                It.IsAny<ThemeVersion>(),
                It.IsAny<CancellationToken>()),
            Times.Once);

        themeRepositoryMock.Verify(
            x => x.AddAsync(
                It.IsAny<Theme>(),
                It.IsAny<CancellationToken>()),
            Times.Once);

        themeRepositoryMock.Verify(
            x => x.GetByIdAsync(
                It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Theory(DisplayName = "AddAsync returns expected result when theme was not given.")]
    [InlineData(sbyte.MaxValue)]
    [InlineData(byte.MaxValue)]
    [InlineData(short.MaxValue)]
    [InlineData(ushort.MaxValue)]
    public async Task AddAsync_ReturnsExpectedResult_WhenThemeNotGiven(int seed)
    {
        var sporadic = new Random(seed);

        // Arrange
        var themeValidatorMock = new Mock<IThemeValidator>();
        var themeRepositoryMock = new Mock<IThemeRepository>();
        var themeVersionRepositoryMock = new Mock<IThemeVersionRepository>();
        var userRepositoryMock = new Mock<IUserRepository>();

        var themeVersion = ModelGenerator.GenerateThemeVersion(sporadic);
        themeVersion.ThemeId = default;
        var user = ModelGenerator.GenerateUser(sporadic);

        userRepositoryMock.Setup(x => x.GetByIdAsync(
            It.IsAny<Guid>(),
            It.IsAny<CancellationToken>())
        ).ReturnsAsync(user);

        themeValidatorMock.Setup(x => x.HasChanges(
            It.IsAny<ThemeVersion>(),
            It.IsAny<CancellationToken>())
        ).ReturnsAsync(true);

        var themeService = new ThemeService(themeValidatorMock.Object, themeRepositoryMock.Object, themeVersionRepositoryMock.Object, userRepositoryMock.Object);

        // Act/Assert
        var exception = await Assert.ThrowsAsync<EntityNotFoundException>(
            () => themeService.AddAsync(themeVersion, CancellationToken.None)
        );

        themeRepositoryMock.Verify(
            x => x.AddAsync(
                It.IsAny<Theme>(),
                It.IsAny<CancellationToken>()),
            Times.Never);

        themeRepositoryMock.Verify(
            x => x.GetByIdAsync(
                It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Theory(DisplayName = "AddAsync returns expected result when user not exist.")]
    [InlineData(sbyte.MaxValue)]
    [InlineData(byte.MaxValue)]
    [InlineData(short.MaxValue)]
    [InlineData(ushort.MaxValue)]
    public async Task AddAsync_ReturnsExpectedResult_WhenUserNotExist(int seed)
    {
        var sporadic = new Random(seed);

        // Arrange
        var themeValidatorMock = new Mock<IThemeValidator>();
        var themeRepositoryMock = new Mock<IThemeRepository>();
        var themeVersionRepositoryMock = new Mock<IThemeVersionRepository>();
        var userRepositoryMock = new Mock<IUserRepository>();

        var theme = ModelGenerator.GenerateTheme(sporadic);
        var themeVersion = ModelGenerator.GenerateThemeVersion(sporadic);
        var user = ModelGenerator.GenerateUser(sporadic);

        themeRepositoryMock.Setup(x => x.GetByIdAsync(
            It.IsAny<Guid>(),
            It.IsAny<CancellationToken>())
        ).ReturnsAsync(theme);

        userRepositoryMock.Setup(x => x.GetByIdAsync(
            It.IsAny<Guid>(),
            It.IsAny<CancellationToken>())
        ).ReturnsAsync((User?)null);

        themeValidatorMock.Setup(x => x.HasChanges(
            It.IsAny<ThemeVersion>(),
            It.IsAny<CancellationToken>())
        ).ReturnsAsync(true);

        var themeService = new ThemeService(themeValidatorMock.Object, themeRepositoryMock.Object, themeVersionRepositoryMock.Object, userRepositoryMock.Object);

        // Act/Assert
        var exception = await Assert.ThrowsAsync<EntityNotFoundException>(
            () => themeService.AddAsync(themeVersion, CancellationToken.None)
        );
    }
    [Theory(DisplayName = "AddAsync returns expected result when theme version has no changes.")]
    [InlineData(sbyte.MaxValue)]
    [InlineData(byte.MaxValue)]
    [InlineData(short.MaxValue)]
    [InlineData(ushort.MaxValue)]
    public async Task AddAsync_ReturnsExpectedResult_WhenNoChanges(int seed)
    {
        var sporadic = new Random(seed);

        // Arrange
        var themeValidatorMock = new Mock<IThemeValidator>();
        var themeRepositoryMock = new Mock<IThemeRepository>();
        var themeVersionRepositoryMock = new Mock<IThemeVersionRepository>();
        var userRepositoryMock = new Mock<IUserRepository>();

        var theme = ModelGenerator.GenerateTheme(sporadic);
        var themeVersion = ModelGenerator.GenerateThemeVersion(sporadic);
        themeVersion.Theme = theme;
        themeVersion.ThemeId = default;
        var user = ModelGenerator.GenerateUser(sporadic);

        themeRepositoryMock.Setup(x => x.AddAsync(
            It.IsAny<Theme>(),
            It.IsAny<CancellationToken>())
        ).ReturnsAsync(theme);

        userRepositoryMock.Setup(x => x.GetByIdAsync(
            It.IsAny<Guid>(),
            It.IsAny<CancellationToken>())
        ).ReturnsAsync(user);

        themeValidatorMock.Setup(x => x.HasChanges(
            It.IsAny<ThemeVersion>(),
            It.IsAny<CancellationToken>())
        ).ReturnsAsync(false);

        var themeService = new ThemeService(themeValidatorMock.Object, themeRepositoryMock.Object, themeVersionRepositoryMock.Object, userRepositoryMock.Object);

        // Act
        var result = await themeService.AddAsync(themeVersion, CancellationToken.None);

        // Assert
        themeVersionRepositoryMock.Verify(
            x => x.AddAsync(
                It.IsAny<ThemeVersion>(),
                It.IsAny<CancellationToken>()),
            Times.Never);
    }
}
