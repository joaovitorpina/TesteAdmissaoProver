using System.Text.Json;
using Amazon.SQS;
using Amazon.SQS.Model;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using TesteAdmissional.Application.Commands;
using TesteAdmissional.Application.Dtos.SQS;
using TesteAdmissional.Application.Dtos.SQS.Enum;
using TesteAdmissional.Application.Ports;
using TesteAdmissional.Domain.Aggregates;
using AmazonSQSConfig = TesteAdmissional.Infra.Environment.Configurations.AmazonSQSConfig;

namespace TesteAdmissional.Infra.Bus.SQS;

public class ContatoProcessor : BackgroundService
{
    private readonly IOptions<AmazonSQSConfig> _sqsConfig;
    private readonly IContatoRepository _contatoRepository;

    public ContatoProcessor(IOptions<AmazonSQSConfig> sqsConfig, IContatoRepository contatoRepository)
    {
        _sqsConfig = sqsConfig;
        _contatoRepository = contatoRepository;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine("Contato Processor starting up");

        var client = new AmazonSQSClient();
        
        while (!stoppingToken.IsCancellationRequested)
        {
            var request = new ReceiveMessageRequest()
            {
                QueueUrl = _sqsConfig.Value.SQSUrl
            };
            
            var response = await client.ReceiveMessageAsync(request, stoppingToken);
            
            foreach (var deserializedBody in response.Messages.Select(message => JsonSerializer.Deserialize<ContatoSqsMessage>(message.Body)))
            {
                switch (deserializedBody!.MessageType)
                {
                    case MessageType.Create:
                        var createCommand = JsonSerializer.Deserialize<CreateContatoCommand>(deserializedBody.Body);
                        await CreateContato(createCommand!);
                        break;
                    case MessageType.Update:
                        var updateCommand = JsonSerializer.Deserialize<UpdateContatoCommand>(deserializedBody.Body);
                        await UpdateContato(updateCommand!);
                        break;
                    case MessageType.Delete:
                        var deleteCommand = JsonSerializer.Deserialize<DeleteContatoCommand>(deserializedBody.Body);
                        await DeleteContato(deleteCommand!);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(deserializedBody.MessageType));
                }
            }
        }
        
        Console.WriteLine("Contato Processor turning off");
    }

    private async Task CreateContato(CreateContatoCommand request)
    {
        var contato = new Contato($"{request.Nome}-PROVER", request.Telefone, request.DataNascimento, request.Sexo,
            new Cargo(request.Cargo));

        await _contatoRepository.Create(contato);
    }

    private async Task UpdateContato(UpdateContatoCommand request)
    {
        var contato = new Contato(request.Nome, request.Telefone, request.DataNascimento, request.Sexo,
            new Cargo(request.Cargo));

        await _contatoRepository.Update(contato);
    }

    private async Task DeleteContato(DeleteContatoCommand request)
    {
        await _contatoRepository.Delete(request.Id);
    }
}