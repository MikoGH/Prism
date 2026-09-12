using Prism.Core.WebApi.Models;

namespace Prism.Core.WebApi.Services.Contracts;

public interface ICalculationRequestProducer
{
    public Task ProduceAsync(CalculationRequest request, CancellationToken token = default);
}
