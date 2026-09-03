using Prism.Core.Domain.Models;

namespace Prism.Core.WebApi.Validators.Contracts;

public interface IThemeValidator
{
    public Task<bool> HasChanges(ThemeVersion themeVersion, CancellationToken token);
}
