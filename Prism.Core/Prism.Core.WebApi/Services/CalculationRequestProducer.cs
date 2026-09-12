using Confluent.Kafka;
using Microsoft.Extensions.Options;
using Prism.Core.WebApi.Models;
using Prism.Core.WebApi.Models.Options;
using Prism.Core.WebApi.Services.Contracts;
using System.Text.Json;

namespace Prism.Core.WebApi.Services;

public class CalculationRequestProducer : ICalculationRequestProducer, IDisposable
{
    private readonly IProducer<string, string> _producer;
    private readonly KafkaOptions _options;
    private readonly ILogger<CalculationRequestProducer> _logger;

    public CalculationRequestProducer(IOptions<KafkaOptions> options, ILogger<CalculationRequestProducer> logger)
    {
        _options = options.Value;
        _logger = logger;

        var config = new ProducerConfig
        {
            BootstrapServers = _options.BootstrapServers,
            Acks = Acks.All,
            EnableIdempotence = true
        };

        _producer = new ProducerBuilder<string, string>(config).Build();
    }

    public async Task ProduceAsync(CalculationRequest request, CancellationToken token = default)
    {
        var json = JsonSerializer.Serialize(request);

        var message = new Message<string, string>
        {
            Key = request.UserId.ToString(),
            Value = json
        };

        try
        {
            var result = await _producer.ProduceAsync(_options.RequestsTopic, message, token);
            _logger.LogInformation(
                "Sended to {Topic} [P{Partition}] @ {Offset} for UserId={UserId}",
                result.Topic, result.Partition.Value, result.Offset.Value, request.UserId);
        }
        catch (ProduceException<string, string> ex)
        {
            _logger.LogError(ex, "Error sending message for UserId={UserId}", request.UserId);
            throw;
        }
    }

    public void Dispose()
    {
        _producer.Flush(TimeSpan.FromSeconds(5));
        _producer.Dispose();
    }
}
