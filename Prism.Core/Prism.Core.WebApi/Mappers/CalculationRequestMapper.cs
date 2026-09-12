using Prism.Core.WebApi.Dtos.Calculation;
using Prism.Core.WebApi.Models;
using Riok.Mapperly.Abstractions;

namespace Prism.Core.WebApi.Mappers;

[Mapper]
public partial class CalculationRequestMapper
{
    public partial CalculationRequest ToCalculationRequest(CalculationRequestDto calculationRequestDto);
}
