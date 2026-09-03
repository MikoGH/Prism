using Monq.Core.Paging.Models;
using Prism.Core.Domain.Models;
using Prism.Core.Domain.Models.Filters;
using Prism.Core.Domain.Models.Includes;
using System.Linq.Expressions;

namespace Prism.Core.Domain.Contracts;

public interface IThemeFieldRepository
{
    public Task<IEnumerable<ThemeField>> FilterAsync(ThemeFieldFilter filter, PagingModel? paging = null, ThemeFieldInclude? include = null, CancellationToken token = default);

    public Task<ThemeField?> FirstOrDefaultAsync(Expression<Func<ThemeField, bool>> predicate, ThemeFieldInclude? include = null, CancellationToken token = default);

    public Task<ThemeField?> GetByIdAsync(Guid id, ThemeFieldInclude? include = null, CancellationToken token = default);

    public Task<ThemeField?> AddAsync(ThemeField themeField, CancellationToken token = default);
}
