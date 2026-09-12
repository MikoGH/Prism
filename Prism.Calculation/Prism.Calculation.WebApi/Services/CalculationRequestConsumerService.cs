using Confluent.Kafka;
using Microsoft.Extensions.Options;
using Prism.Calculation.WebApi.Models;
using Prism.Calculation.WebApi.Models.Options;
using System.Text.Json;

namespace Prism.Calculation.WebApi.Services;

public class CalculationRequestConsumerService : BackgroundService
{
    private readonly KafkaOptions _options;
    private readonly IConsumer<string, string> _consumer;
    private readonly ILogger<CalculationRequestConsumerService> _logger;

    public CalculationRequestConsumerService(IOptions<KafkaOptions> options, ILogger<CalculationRequestConsumerService> logger)
    {
        _options = options.Value;
        var consumerConfig = new ConsumerConfig
        {
            BootstrapServers = _options.BootstrapServers,
            GroupId = _options.ConsumerGroup,
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnableAutoCommit = false
        };
        _consumer = new ConsumerBuilder<string, string>(consumerConfig).Build();
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _consumer.Subscribe(_options.RequestsTopic);
        _logger.LogInformation("Consumer subscribed to {Topic}", _options.RequestsTopic);

        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                ConsumeResult<string, string>? result = null;

                try
                {
                    result = await Task.Run(() => _consumer.Consume(stoppingToken), stoppingToken);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (ConsumeException ex)
                {
                    _logger.LogError(ex, "Consuming exception");
                    continue;
                }

                if (result?.Message?.Value is null) continue;

                _logger.LogInformation("Message received: {Message}", result.Message);

                try
                {
                    var request = JsonSerializer.Deserialize<CalculationRequest>(result.Message.Value);

                    if (request is null)
                    {
                        _logger.LogWarning("Skip empty message.");

                        _consumer.Commit(result);
                        return;
                    }

                    _logger.LogInformation("Message deserialized. User ID: {Id}", request.UserId);

                    _consumer.Commit(result);
                }
                catch (JsonException ex)
                {
                    _logger.LogError(ex, "Deserialization exception");

                    _consumer.Commit(result);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Calculation exception");
                }
            }
        }
        finally
        {
            _consumer.Close();
        }
    }
}
