using Flequery.Models;
using Prism.Core.Domain.Models;
using System.Linq.Expressions;

namespace Prism.Core.Domain.Contracts;

public interface IUserRepository
{
    public Task<PagedResponse<User>> FilterAsync(QueryRequest request, CancellationToken token = default);

    public Task<User?> FirstOrDefaultAsync(Expression<Func<User, bool>> predicate, CancellationToken token = default);

    public Task<User?> GetByIdAsync(Guid id, CancellationToken token = default);

    public Task<User?> AddAsync(User user, CancellationToken token = default);

    public Task<User?> UpdateAsync(Guid id, User user, CancellationToken token = default);

    public Task<bool> DeleteAsync(Guid id, CancellationToken token = default);
}
