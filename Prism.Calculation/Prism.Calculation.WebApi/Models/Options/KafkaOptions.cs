namespace Prism.Calculation.WebApi.Models.Options;

public class KafkaOptions
{
    public string BootstrapServers { get; set; }
    public string RequestsTopic { get; set; }
    public string ConsumerGroup { get; set; }
}
