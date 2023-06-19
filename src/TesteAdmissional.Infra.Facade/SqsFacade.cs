using System.Text.Json;
using Amazon.SQS;
using Amazon.SQS.Model;
using Microsoft.Extensions.Options;
using TesteAdmissional.Application.Ports;
using AmazonSQSConfig = TesteAdmissional.Infra.Environment.Configurations.AmazonSQSConfig;

namespace TesteAdmissional.Infra.Facade;

public class SqsFacade : ISqsFacade
{
    private readonly AmazonSQSClient _amazonSqsClient;
    private readonly IOptions<AmazonSQSConfig> _config;

    public SqsFacade(IOptions<AmazonSQSConfig> config)
    {
        _config = config;
        _amazonSqsClient = new AmazonSQSClient();
    }

    public async Task SendMessage(string body)
    {
        var request = new SendMessageRequest()
        {
            MessageBody = body,
            QueueUrl = _config.Value.SQSUrl
        };

        await _amazonSqsClient.SendMessageAsync(request);
    }
}