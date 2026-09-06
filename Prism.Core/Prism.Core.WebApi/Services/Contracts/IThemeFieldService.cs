using Monq.Core.Paging.Models;
using Prism.Core.Domain.Models;
using Prism.Core.Domain.Models.Filters;

namespace Prism.Core.WebApi.Services.Contracts;

public interface IThemeFieldService
{
    public Task<IEnumerable<ThemeFieldVersion>> FilterAsync(PagingModel paging, ThemeFieldVersionFilter filter, CancellationToken token);

    public Task<IEnumerable<ThemeFieldVersion>> FilterActualAsync(PagingModel paging, ThemeFieldVersionFilter filter, CancellationToken token);

    public Task<ThemeFieldVersion?> GetByIdAsync(Guid id, CancellationToken token);

    public Task BatchAsync(IEnumerable<ThemeFieldVersion> themeFieldVersions, Guid themeId, CancellationToken token);
}
