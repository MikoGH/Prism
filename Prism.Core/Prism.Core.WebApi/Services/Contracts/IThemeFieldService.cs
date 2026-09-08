using Monq.Core.Paging.Models;
using Prism.Core.Domain.Models;
using Prism.Core.Domain.Models.Filters;
using Prism.Core.Domain.Models.Includes;

namespace Prism.Core.WebApi.Services.Contracts;

public interface IThemeFieldService
{
    public Task<IEnumerable<ThemeFieldVersion>> FilterAsync(ThemeFieldVersionFilter filter, PagingModel paging, ThemeFieldVersionInclude include, CancellationToken token);

    public Task<IEnumerable<ThemeFieldVersion>> FilterActualAsync(ThemeFieldVersionFilter filter, PagingModel paging, ThemeFieldVersionInclude include, CancellationToken token);

    public Task<ThemeFieldVersion?> GetByIdAsync(Guid id, ThemeFieldVersionInclude include, CancellationToken token);

    public Task BatchAsync(IEnumerable<ThemeFieldVersion> themeFieldVersions, Guid themeId, CancellationToken token);
}
