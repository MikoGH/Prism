using Flequery.Models;
using Prism.Core.Domain.Models;
using System.Linq.Expressions;

namespace Prism.Core.Domain.Contracts;

public interface IThemeFieldVersionRepository
{
    public Task<PagedResponse<ThemeFieldVersion>> FilterAsync(QueryRequest request, CancellationToken token = default);

    public Task<PagedResponse<ThemeFieldVersion>> FilterActualAsync(QueryRequest request, CancellationToken token = default);

    public Task<ThemeFieldVersion?> FirstOrDefaultAsync(Expression<Func<ThemeFieldVersion, bool>> predicate, IncludeRequest? include = null, CancellationToken token = default);

    public Task<ThemeFieldVersion?> FirstOrDefaultActualAsync(Expression<Func<ThemeFieldVersion, bool>> predicate, IncludeRequest? include = null, CancellationToken token = default);

    public Task<ThemeFieldVersion?> GetByIdAsync(Guid id, IncludeRequest? include = null, CancellationToken token = default);

    public Task BatchAsync(IEnumerable<ThemeFieldVersion> themeFieldVersions, CancellationToken token = default);

    public Task<ThemeFieldVersion?> AddAsync(ThemeFieldVersion themeFieldVersion, CancellationToken token = default);
}
