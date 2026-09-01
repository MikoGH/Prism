using Prism.Core.WebApi.Models;

namespace Prism.Core.WebApi.Repositories.Abstractions;

public interface IRateRepository
{
    public Task<IEnumerable<Rate>> GetAllAsync(CancellationToken token);
}
