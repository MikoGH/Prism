using Microsoft.EntityFrameworkCore;
using Monq.Core.MvcExtensions.Extensions;
using Monq.Core.Paging.Extensions;
using Monq.Core.Paging.Models;
using Prism.Core.DataAccess.Database;
using Prism.Core.Domain.Contracts;
using Prism.Core.Domain.Models;
using Prism.Core.Domain.Models.Filters;
using System.Linq.Expressions;

namespace Prism.Core.DataAccess.Repositories;

public class UserRepository : IUserRepository
{
    private readonly PrismContext _context;

    public UserRepository(PrismContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> FilterAsync(PagingModel paging, UserFilter filter, CancellationToken token)
    {
        var users = await _context.Users
            .AsNoTracking()
            .FilterBy(filter)
            .WithPaging(paging, null, x => x.Name)
            .ToListAsync(token);

        return users;
    }

    public Task<User?> FirstOrDefaultAsync(Expression<Func<User, bool>> predicate, CancellationToken token)
    {
        var user = _context.Users
            .AsNoTracking()
            .Where(predicate)
            .FirstOrDefaultAsync(token);

        return user;
    }

    public Task<User?> GetAsync(Guid id, CancellationToken token)
    {
        return _context.Users.FirstOrDefaultAsync(x => x.Id == id, token);
    }

    public async Task<User?> AddAsync(User user, CancellationToken token)
    {
        var addedUser = await _context.AddAsync(user, token);
        await _context.SaveChangesAsync(token);

        return addedUser.Entity;
    }

    public async Task<User?> UpdateAsync(Guid id, User user, CancellationToken token)
    {
        var existingUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == id, token);

        if (existingUser is null)
            return null;

        existingUser.Role = user.Role;
        existingUser.Name = user.Name;
        existingUser.PasswordHash = user.PasswordHash;

        _context.Users.Update(existingUser);
        await _context.SaveChangesAsync(token);

        return existingUser;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken token)
    {
        var existingUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == id, token);

        if (existingUser is null)
            return false;

        _context.Users.Remove(existingUser);
        await _context.SaveChangesAsync(token);

        return true;
    }
}
