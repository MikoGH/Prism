using Flequery.Models;
using Prism.Core.Domain.Models;
using System.Linq.Expressions;

namespace Prism.Core.Domain.Contracts;

public interface IThemeVersionRepository
{
    public Task<PagedResponse<ThemeVersion>> FilterAsync(QueryRequest request, CancellationToken token = default);

    public Task<PagedResponse<ThemeVersion>> FilterActualAsync(QueryRequest request, CancellationToken token = default);

    public Task<ThemeVersion?> FirstOrDefaultAsync(Expression<Func<ThemeVersion, bool>> predicate, IncludeRequest? include = null, CancellationToken token = default);

    public Task<ThemeVersion?> FirstOrDefaultActualAsync(Expression<Func<ThemeVersion, bool>> predicate, IncludeRequest? include = null, CancellationToken token = default);

    public Task<ThemeVersion?> GetByIdAsync(Guid id, IncludeRequest? include = null, CancellationToken token = default);

    public Task<ThemeVersion?> AddAsync(ThemeVersion themeVersion, CancellationToken token = default);
}
