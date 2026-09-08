using Flequery.Models;
using Prism.Core.Domain.Models;
using System.Linq.Expressions;

namespace Prism.Core.Domain.Contracts;

public interface IThemeFieldRepository
{
    public Task<PagedResponse<ThemeField>> FilterAsync(QueryRequest request, CancellationToken token = default);

    public Task<ThemeField?> FirstOrDefaultAsync(Expression<Func<ThemeField, bool>> predicate, IncludeRequest? include = null, CancellationToken token = default);

    public Task<ThemeField?> GetByIdAsync(Guid id, IncludeRequest? include = null, CancellationToken token = default);

    public Task<ThemeField?> AddAsync(ThemeField themeField, CancellationToken token = default);
}
