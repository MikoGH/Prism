using Monq.Core.Paging.Models;
using Prism.Core.Domain.Models;
using Prism.Core.Domain.Models.Filters;
using Prism.Core.Domain.Models.Includes;
using System.Linq.Expressions;

namespace Prism.Core.Domain.Contracts;

public interface IThemeFieldVersionRepository
{
    public Task<IEnumerable<ThemeFieldVersion>> FilterAsync(ThemeFieldVersionFilter filter, PagingModel? paging = null, ThemeFieldVersionInclude? include = null, CancellationToken token = default);

    public Task<IEnumerable<ThemeFieldVersion>> FilterActualAsync(ThemeFieldVersionFilter filter, PagingModel? paging = null, ThemeFieldVersionInclude? include = null, CancellationToken token = default);

    public Task<ThemeFieldVersion?> FirstOrDefaultAsync(Expression<Func<ThemeFieldVersion, bool>> predicate, ThemeFieldVersionInclude? include = null, CancellationToken token = default);

    public Task<ThemeFieldVersion?> FirstOrDefaultActualAsync(Expression<Func<ThemeFieldVersion, bool>> predicate, ThemeFieldVersionInclude? include = null, CancellationToken token = default);

    public Task<ThemeFieldVersion?> GetByIdAsync(Guid id, ThemeFieldVersionInclude? include = null, CancellationToken token = default);

    public Task BatchAsync(IEnumerable<ThemeFieldVersion> themeFieldVersions, CancellationToken token = default);

    public Task<ThemeFieldVersion?> AddAsync(ThemeFieldVersion themeFieldVersion, CancellationToken token = default);
}
