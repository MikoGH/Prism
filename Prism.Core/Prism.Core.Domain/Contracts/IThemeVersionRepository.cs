using Monq.Core.Paging.Models;
using Prism.Core.Domain.Models;
using Prism.Core.Domain.Models.Filters;
using Prism.Core.Domain.Models.Includes;
using System.Linq.Expressions;

namespace Prism.Core.Domain.Contracts;

public interface IThemeVersionRepository
{
    public Task<IEnumerable<ThemeVersion>> FilterAsync(ThemeVersionFilter filter, PagingModel? paging = null, ThemeVersionInclude? include = null, CancellationToken token = default);

    public Task<IEnumerable<ThemeVersion>> FilterActualAsync(ThemeVersionFilter filter, PagingModel? paging = null, ThemeVersionInclude? include = null, CancellationToken token = default);

    public Task<ThemeVersion?> FirstOrDefaultAsync(Expression<Func<ThemeVersion, bool>> predicate, ThemeVersionInclude? include = null, CancellationToken token = default);

    public Task<ThemeVersion?> FirstOrDefaultActualAsync(Expression<Func<ThemeVersion, bool>> predicate, ThemeVersionInclude? include = null, CancellationToken token = default);

    public Task<ThemeVersion?> GetByIdAsync(Guid id, ThemeVersionInclude? include = null, CancellationToken token = default);

    public Task<ThemeVersion?> AddAsync(ThemeVersion themeVersion, CancellationToken token = default);
}
