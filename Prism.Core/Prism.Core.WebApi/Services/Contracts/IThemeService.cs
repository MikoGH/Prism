using Flequery.Models;
using Prism.Core.Domain.Models;

namespace Prism.Core.WebApi.Services.Contracts;

public interface IThemeService
{
    public Task<PagedResponse<ThemeVersion>> FilterAsync(QueryRequest request, CancellationToken token);

    public Task<PagedResponse<ThemeVersion>> FilterActualAsync(QueryRequest request, CancellationToken token);

    public Task<ThemeVersion?> GetByIdAsync(Guid id, IncludeRequest include, CancellationToken token);

    public Task<ThemeVersion?> AddAsync(ThemeVersion themeVersion, CancellationToken token);

    public Task<ThemeVersion?> DeleteAsync(Guid themeId, Guid userId, CancellationToken token);

    public Task<bool> HasChangesAsync(ThemeVersion themeVersion, CancellationToken token);
}
