using Monq.Core.Paging.Models;
using Prism.Core.Domain.Models;
using Prism.Core.Domain.Models.Filters;

namespace Prism.Core.WebApi.Services.Contracts;

public interface IThemeService
{
    public Task<IEnumerable<ThemeVersion>> FilterAsync(ThemeVersionFilter filter, PagingModel paging, CancellationToken token);

    public Task<IEnumerable<ThemeVersion>> FilterActualAsync(ThemeVersionFilter filter, PagingModel paging, CancellationToken token);

    public Task<ThemeVersion?> GetByIdAsync(Guid id, CancellationToken token);

    public Task<ThemeVersion?> AddAsync(ThemeVersion themeVersion, CancellationToken token);

    public Task<ThemeVersion?> DeleteAsync(Guid themeId, Guid userId, CancellationToken token);

    public Task<bool> HasChangesAsync(ThemeVersion themeVersion, CancellationToken token);
}
