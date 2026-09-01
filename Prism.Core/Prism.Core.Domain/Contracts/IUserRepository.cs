using Monq.Core.Paging.Models;
using Prism.Core.Domain.Models;
using Prism.Core.Domain.Models.Filters;
using System.Linq.Expressions;

namespace Prism.Core.Domain.Contracts;

public interface IUserRepository
{
    public Task<IEnumerable<User>> FilterAsync(PagingModel paging, UserFilter filter, CancellationToken token);

    public Task<User?> FirstOrDefaultAsync(Expression<Func<User, bool>> predicate, CancellationToken token);

    public Task<User?> GetAsync(Guid id, CancellationToken token);

    public Task<User?> AddAsync(User user, CancellationToken token);

    public Task<User?> UpdateAsync(Guid id, User user, CancellationToken token);

    public Task<bool> DeleteAsync(Guid id, CancellationToken token);
}
