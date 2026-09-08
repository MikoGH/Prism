using Flequery.Models;
using Prism.Core.Domain.Models;

namespace Prism.Core.WebApi.Services.Contracts;

public interface IThemeFieldService
{
    public Task<PagedResponse<ThemeFieldVersion>> FilterAsync(QueryRequest request, CancellationToken token);

    public Task<PagedResponse<ThemeFieldVersion>> FilterActualAsync(QueryRequest request, CancellationToken token);

    public Task<ThemeFieldVersion?> GetByIdAsync(Guid id, IncludeRequest include, CancellationToken token);

    public Task BatchAsync(IEnumerable<ThemeFieldVersion> themeFieldVersions, Guid themeId, CancellationToken token);
}
