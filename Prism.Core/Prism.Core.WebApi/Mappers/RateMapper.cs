using Prism.Core.Domain.Models;
using Prism.Core.WebApi.Dtos.Rate;
using Riok.Mapperly.Abstractions;

namespace Prism.Core.WebApi.Mappers;

[Mapper]
public partial class RateMapper
{
    public partial RateDto ToRateDto(Rate rate);
}
