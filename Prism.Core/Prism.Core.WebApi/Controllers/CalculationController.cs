using Microsoft.AspNetCore.Mvc;
using Prism.Core.WebApi.Constants;
using Prism.Core.WebApi.Dtos.Calculation;
using Prism.Core.WebApi.Mappers;
using Prism.Core.WebApi.Services.Contracts;

namespace Prism.Core.WebApi.Controllers;

[Controller]
[Route($"{AppConstants.RoutePrefix}/[controller]")]
public class CalculationController : ControllerBase
{
    private readonly ICalculationRequestProducer _producer;
    private readonly CalculationRequestMapper _mapper;

    public CalculationController(ICalculationRequestProducer producer, CalculationRequestMapper mapper)
    {
        _producer = producer;
        _mapper = mapper;
    }

    [HttpPost()]
    public async Task<ActionResult> SendCalculationRequest([FromBody] CalculationRequestDto calculationRequestDto, CancellationToken token)
    {
        var calculationRequest = _mapper.ToCalculationRequest(calculationRequestDto);

        await _producer.ProduceAsync(calculationRequest, token);

        return Ok();
    }
}
