using Prism.Core.WebApi.Models;

namespace Prism.Core.WebApi.Repositories.Abstractions;

public interface IRateRepository
{
    public IEnumerable<Rate> GetAllRates(CancellationToken token);
}
