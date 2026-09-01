using Microsoft.AspNetCore.Mvc;
using Prism.Core.WebApi.Constants;
using Prism.Core.WebApi.Dtos.Rate;
using Prism.Core.WebApi.Mappers;
using Prism.Core.WebApi.Repositories.Abstractions;

namespace Prism.Core.WebApi.Controllers;

[Controller]
[Route($"{AppConstants.RoutePrefix}/[controller]")]
public class RateController : ControllerBase
{
    private readonly RateMapper _mapper;
    private readonly IRateRepository _rateRepository;

    public RateController(RateMapper mapper, IRateRepository rateRepository)
    {
        _mapper = mapper;
        _rateRepository = rateRepository;
    }

    [HttpGet()]
    public async Task<ActionResult<IEnumerable<RateDto>>> GetAllRatesAsync(CancellationToken token)
    {
        var rates = await _rateRepository.GetAllAsync(token);

        var rateDtos = rates.Select(x => _mapper.ToRateDto(x)).ToList();

        return Ok(rateDtos);
    }
}
