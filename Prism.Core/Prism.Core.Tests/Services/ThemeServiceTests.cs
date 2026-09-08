using Flequery.Models;
using Moq;
using Prism.Core.Domain.Contracts;
using Prism.Core.Domain.Models;
using Prism.Core.Tests.Helpers;
using Prism.Core.WebApi.Exceptions;
using Prism.Core.WebApi.Models;
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
        var lastThemeVersion = ModelGenerator.GenerateThemeVersion(sporadic);
        lastThemeVersion.Id = (Guid)themeVersion.PreviousVersionId!;
        var user = ModelGenerator.GenerateUser(sporadic);
        var validationResult = new ValidationResult();

        themeVersionRepositoryMock.Setup(x => x.GetByIdAsync(
            It.IsAny<Guid>(),
            It.IsAny<IncludeRequest>(),
            It.IsAny<CancellationToken>())
        ).ReturnsAsync(lastThemeVersion);

        themeRepositoryMock.Setup(x => x.GetByIdAsync(
            It.IsAny<Guid>(),
            It.IsAny<CancellationToken>())
        ).ReturnsAsync(theme);

        userRepositoryMock.Setup(x => x.GetByIdAsync(
            It.IsAny<Guid>(),
            It.IsAny<CancellationToken>())
        ).ReturnsAsync(user);

        themeValidatorMock.Setup(x => x.Validate(
            It.IsAny<ThemeVersion>())
        ).Returns(validationResult);

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
        var lastThemeVersion = ModelGenerator.GenerateThemeVersion(sporadic);
        lastThemeVersion.Id = (Guid)themeVersion.PreviousVersionId!;
        var user = ModelGenerator.GenerateUser(sporadic);
        var validationResult = new ValidationResult();

        themeVersionRepositoryMock.Setup(x => x.GetByIdAsync(
            It.IsAny<Guid>(),
            It.IsAny<IncludeRequest>(),
            It.IsAny<CancellationToken>())
        ).ReturnsAsync(lastThemeVersion);

        themeRepositoryMock.Setup(x => x.AddAsync(
            It.IsAny<Theme>(),
            It.IsAny<CancellationToken>())
        ).ReturnsAsync(theme);

        userRepositoryMock.Setup(x => x.GetByIdAsync(
            It.IsAny<Guid>(),
            It.IsAny<CancellationToken>())
        ).ReturnsAsync(user);

        themeValidatorMock.Setup(x => x.Validate(
            It.IsAny<ThemeVersion>())
        ).Returns(validationResult);

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
        var validationResult = new ValidationResult();

        var themeVersion = ModelGenerator.GenerateThemeVersion(sporadic);
        themeVersion.ThemeId = default;
        var lastThemeVersion = ModelGenerator.GenerateThemeVersion(sporadic);
        lastThemeVersion.Id = (Guid)themeVersion.PreviousVersionId!;
        var user = ModelGenerator.GenerateUser(sporadic);

        themeVersionRepositoryMock.Setup(x => x.GetByIdAsync(
            It.IsAny<Guid>(),
            It.IsAny<IncludeRequest>(),
            It.IsAny<CancellationToken>())
        ).ReturnsAsync(lastThemeVersion);

        userRepositoryMock.Setup(x => x.GetByIdAsync(
            It.IsAny<Guid>(),
            It.IsAny<CancellationToken>())
        ).ReturnsAsync(user);

        themeValidatorMock.Setup(x => x.Validate(
            It.IsAny<ThemeVersion>())
        ).Returns(validationResult);

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
        var lastThemeVersion = ModelGenerator.GenerateThemeVersion(sporadic);
        lastThemeVersion.Id = (Guid)themeVersion.PreviousVersionId!;
        var user = ModelGenerator.GenerateUser(sporadic);
        var validationResult = new ValidationResult();

        themeVersionRepositoryMock.Setup(x => x.GetByIdAsync(
            It.IsAny<Guid>(),
            It.IsAny<IncludeRequest>(),
            It.IsAny<CancellationToken>())
        ).ReturnsAsync(lastThemeVersion);

        themeRepositoryMock.Setup(x => x.GetByIdAsync(
            It.IsAny<Guid>(),
            It.IsAny<CancellationToken>())
        ).ReturnsAsync(theme);

        userRepositoryMock.Setup(x => x.GetByIdAsync(
            It.IsAny<Guid>(),
            It.IsAny<CancellationToken>())
        ).ReturnsAsync((User?)null);

        themeValidatorMock.Setup(x => x.Validate(
            It.IsAny<ThemeVersion>())
        ).Returns(validationResult);

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
        var lastThemeVersion = themeVersion;
        lastThemeVersion.Id = (Guid)themeVersion.PreviousVersionId!;
        var user = ModelGenerator.GenerateUser(sporadic);
        var validationResult = new ValidationResult();

        themeVersionRepositoryMock.Setup(x => x.GetByIdAsync(
            It.IsAny<Guid>(),
            It.IsAny<IncludeRequest>(),
            It.IsAny<CancellationToken>())
        ).ReturnsAsync(lastThemeVersion);

        themeRepositoryMock.Setup(x => x.GetByIdAsync(
            It.IsAny<Guid>(),
            It.IsAny<CancellationToken>())
        ).ReturnsAsync(theme);

        userRepositoryMock.Setup(x => x.GetByIdAsync(
            It.IsAny<Guid>(),
            It.IsAny<CancellationToken>())
        ).ReturnsAsync(user);

        themeValidatorMock.Setup(x => x.Validate(
            It.IsAny<ThemeVersion>())
        ).Returns(validationResult);

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

    [Theory(DisplayName = "HasChanges returns expected result when changed.")]
    [InlineData(sbyte.MaxValue)]
    [InlineData(byte.MaxValue)]
    [InlineData(short.MaxValue)]
    [InlineData(ushort.MaxValue)]
    public async Task HasChanges_ReturnsExpectedResult_WhenChanged(int seed)
    {
        var sporadic = new Random(seed);

        // Arrange
        var themeValidatorMock = new Mock<IThemeValidator>();
        var themeRepositoryMock = new Mock<IThemeRepository>();
        var themeVersionRepositoryMock = new Mock<IThemeVersionRepository>();
        var userRepositoryMock = new Mock<IUserRepository>();

        var themeVersion = ModelGenerator.GenerateThemeVersion(sporadic);
        var lastThemeVersion = ModelGenerator.GenerateThemeVersion(sporadic);
        lastThemeVersion.Id = (Guid)themeVersion.PreviousVersionId!;

        themeVersionRepositoryMock.Setup(x => x.GetByIdAsync(
            It.IsAny<Guid>(),
            It.IsAny<IncludeRequest>(),
            It.IsAny<CancellationToken>())
        ).ReturnsAsync(lastThemeVersion);

        var themeService = new ThemeService(themeValidatorMock.Object, themeRepositoryMock.Object, themeVersionRepositoryMock.Object, userRepositoryMock.Object);

        // Act
        var result = await themeService.HasChangesAsync(themeVersion, CancellationToken.None);

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
        var themeValidatorMock = new Mock<IThemeValidator>();
        var themeRepositoryMock = new Mock<IThemeRepository>();
        var themeVersionRepositoryMock = new Mock<IThemeVersionRepository>();
        var userRepositoryMock = new Mock<IUserRepository>();

        var themeVersion = ModelGenerator.GenerateThemeVersion(sporadic);
        var lastThemeVersion = ModelGenerator.GenerateThemeVersion(sporadic);
        lastThemeVersion.Id = (Guid)themeVersion.PreviousVersionId!;

        themeVersion.ThemeId = lastThemeVersion.ThemeId;
        themeVersion.Name = lastThemeVersion.Name;
        themeVersion.UserCreateId = lastThemeVersion.UserCreateId;
        themeVersion.IsAccepted = lastThemeVersion.IsAccepted;
        themeVersion.IsChecked = lastThemeVersion.IsChecked;
        themeVersion.IsDeleted = lastThemeVersion.IsDeleted;

        themeVersionRepositoryMock.Setup(x => x.GetByIdAsync(
            It.IsAny<Guid>(),
            It.IsAny<IncludeRequest>(),
            It.IsAny<CancellationToken>())
        ).ReturnsAsync(lastThemeVersion);

        var themeService = new ThemeService(themeValidatorMock.Object, themeRepositoryMock.Object, themeVersionRepositoryMock.Object, userRepositoryMock.Object);

        // Act
        var result = await themeService.HasChangesAsync(themeVersion, CancellationToken.None);

        // Assert
        Assert.False(result);
    }

    [Theory(DisplayName = "HasChanges returns expected result when is checked was changed to true.")]
    [InlineData(sbyte.MaxValue)]
    [InlineData(byte.MaxValue)]
    [InlineData(short.MaxValue)]
    [InlineData(ushort.MaxValue)]
    public async Task HasChanges_ReturnsExpectedResult_WhenIsCheckedChangedToTrue(int seed)
    {
        var sporadic = new Random(seed);

        // Arrange
        var themeValidatorMock = new Mock<IThemeValidator>();
        var themeRepositoryMock = new Mock<IThemeRepository>();
        var themeVersionRepositoryMock = new Mock<IThemeVersionRepository>();
        var userRepositoryMock = new Mock<IUserRepository>();

        var themeVersion = ModelGenerator.GenerateThemeVersion(sporadic);
        var lastThemeVersion = ModelGenerator.GenerateThemeVersion(sporadic);
        lastThemeVersion.Id = (Guid)themeVersion.PreviousVersionId!;

        themeVersion.ThemeId = lastThemeVersion.ThemeId;
        themeVersion.Name = lastThemeVersion.Name;
        themeVersion.UserCreateId = lastThemeVersion.UserCreateId;
        themeVersion.IsDeleted = lastThemeVersion.IsDeleted;
        lastThemeVersion.IsChecked = false;
        themeVersion.IsChecked = true;

        themeVersionRepositoryMock.Setup(x => x.GetByIdAsync(
            It.IsAny<Guid>(),
            It.IsAny<IncludeRequest>(),
            It.IsAny<CancellationToken>())
        ).ReturnsAsync(lastThemeVersion);

        var themeService = new ThemeService(themeValidatorMock.Object, themeRepositoryMock.Object, themeVersionRepositoryMock.Object, userRepositoryMock.Object);

        // Act
        var result = await themeService.HasChangesAsync(themeVersion, CancellationToken.None);

        // Assert
        Assert.True(result);
    }

    [Theory(DisplayName = "HasChanges returns expected result when is checked was changed to false.")]
    [InlineData(sbyte.MaxValue)]
    [InlineData(byte.MaxValue)]
    [InlineData(short.MaxValue)]
    [InlineData(ushort.MaxValue)]
    public async Task HasChanges_ReturnsExpectedResult_WhenIsCheckedChangedToFalse(int seed)
    {
        var sporadic = new Random(seed);

        // Arrange
        var themeValidatorMock = new Mock<IThemeValidator>();
        var themeRepositoryMock = new Mock<IThemeRepository>();
        var themeVersionRepositoryMock = new Mock<IThemeVersionRepository>();
        var userRepositoryMock = new Mock<IUserRepository>();

        var themeVersion = ModelGenerator.GenerateThemeVersion(sporadic);
        var lastThemeVersion = ModelGenerator.GenerateThemeVersion(sporadic);
        lastThemeVersion.Id = (Guid)themeVersion.PreviousVersionId!;

        themeVersion.ThemeId = lastThemeVersion.ThemeId;
        themeVersion.Name = lastThemeVersion.Name;
        themeVersion.UserCreateId = lastThemeVersion.UserCreateId;
        themeVersion.IsDeleted = lastThemeVersion.IsDeleted;
        lastThemeVersion.IsChecked = true;
        themeVersion.IsChecked = false;

        themeVersionRepositoryMock.Setup(x => x.GetByIdAsync(
            It.IsAny<Guid>(),
            It.IsAny<IncludeRequest>(),
            It.IsAny<CancellationToken>())
        ).ReturnsAsync(lastThemeVersion);

        var themeService = new ThemeService(themeValidatorMock.Object, themeRepositoryMock.Object, themeVersionRepositoryMock.Object, userRepositoryMock.Object);

        // Act
        var result = await themeService.HasChangesAsync(themeVersion, CancellationToken.None);

        // Assert
        Assert.False(result);
    }

    [Theory(DisplayName = "HasChanges returns expected result when is accepted was changed.")]
    [InlineData(sbyte.MaxValue)]
    [InlineData(byte.MaxValue)]
    [InlineData(short.MaxValue)]
    [InlineData(ushort.MaxValue)]
    public async Task HasChanges_ReturnsExpectedResult_WhenIsAcceptedChanged(int seed)
    {
        var sporadic = new Random(seed);

        // Arrange
        var themeValidatorMock = new Mock<IThemeValidator>();
        var themeRepositoryMock = new Mock<IThemeRepository>();
        var themeVersionRepositoryMock = new Mock<IThemeVersionRepository>();
        var userRepositoryMock = new Mock<IUserRepository>();

        var themeVersion = ModelGenerator.GenerateThemeVersion(sporadic);
        var lastThemeVersion = ModelGenerator.GenerateThemeVersion(sporadic);
        lastThemeVersion.Id = (Guid)themeVersion.PreviousVersionId!;

        themeVersion.ThemeId = lastThemeVersion.ThemeId;
        themeVersion.Name = lastThemeVersion.Name;
        themeVersion.UserCreateId = lastThemeVersion.UserCreateId;
        themeVersion.IsDeleted = lastThemeVersion.IsDeleted;
        lastThemeVersion.IsChecked = true;
        themeVersion.IsChecked = true;
        lastThemeVersion.IsAccepted = true;
        themeVersion.IsAccepted = false;

        themeVersionRepositoryMock.Setup(x => x.GetByIdAsync(
            It.IsAny<Guid>(),
            It.IsAny<IncludeRequest>(),
            It.IsAny<CancellationToken>())
        ).ReturnsAsync(lastThemeVersion);

        var themeService = new ThemeService(themeValidatorMock.Object, themeRepositoryMock.Object, themeVersionRepositoryMock.Object, userRepositoryMock.Object);

        // Act
        var result = await themeService.HasChangesAsync(themeVersion, CancellationToken.None);

        // Assert
        Assert.True(result);
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
        var themeValidatorMock = new Mock<IThemeValidator>();
        var themeRepositoryMock = new Mock<IThemeRepository>();
        var themeVersionRepositoryMock = new Mock<IThemeVersionRepository>();
        var userRepositoryMock = new Mock<IUserRepository>();

        var themeVersion = ModelGenerator.GenerateThemeVersion(sporadic);

        themeVersionRepositoryMock.Setup(x => x.GetByIdAsync(
            It.IsAny<Guid>(),
            It.IsAny<IncludeRequest>(),
            It.IsAny<CancellationToken>())
        ).ReturnsAsync((ThemeVersion?)null);

        var themeService = new ThemeService(themeValidatorMock.Object, themeRepositoryMock.Object, themeVersionRepositoryMock.Object, userRepositoryMock.Object);

        // Act
        var result = await themeService.HasChangesAsync(themeVersion, CancellationToken.None);

        // Assert
        Assert.True(result);
    }
}
