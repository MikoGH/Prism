using Prism.Core.Domain.Models;

namespace Prism.Core.Domain.Contracts;

public interface IThemeRepository
{
    public Task<IEnumerable<Theme>> GetAllAsync(CancellationToken token = default);

    public Task<Theme?> GetByIdAsync(Guid id, CancellationToken token = default);

    public Task<Theme?> AddAsync(Theme theme, CancellationToken token = default);
}
