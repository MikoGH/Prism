using Monq.Core.Paging.Models;
using Prism.Core.Domain.Models;
using Prism.Core.Domain.Models.Filters;

namespace Prism.Core.WebApi.Services.Contracts;

public interface IThemeService
{
    public Task<IEnumerable<ThemeVersion>> FilterAsync(PagingModel paging, ThemeVersionFilter filter, CancellationToken token);

    public Task<IEnumerable<ThemeVersion>> FilterActualAsync(PagingModel paging, ThemeVersionFilter filter, CancellationToken token);

    public Task<ThemeVersion?> FirstOrDefaultAsync(Guid id, CancellationToken token);

    public Task<ThemeVersion?> AddAsync(ThemeVersion themeVersion, CancellationToken token);

    public Task<ThemeVersion?> DeleteAsync(Guid themeId, Guid userId, CancellationToken token);
}
