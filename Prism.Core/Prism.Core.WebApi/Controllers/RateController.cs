using Microsoft.AspNetCore.Mvc;
using Prism.Core.WebApi.Constants;
using Prism.Core.WebApi.Models;
using Prism.Core.WebApi.Repositories.Abstractions;

namespace Prism.Core.WebApi.Controllers;

[Controller]
[Route($"{AppConstants.RoutePrefix}/[controller]")]
public class RateController : ControllerBase
{
    private readonly IRateRepository _rateRepository;

    public RateController(IRateRepository rateRepository)
    {
        _rateRepository = rateRepository;
    }

    [HttpGet()]
    public async Task<ActionResult<IEnumerable<Rate>>> GetRates(CancellationToken token)
    {
        var rates = _rateRepository.GetAllRates(token);

        return Ok(rates);
    }
}
