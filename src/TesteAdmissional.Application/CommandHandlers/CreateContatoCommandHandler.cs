using System.Text.Json;
using MediatR;
using TesteAdmissional.Application.Commands;
using TesteAdmissional.Application.Dtos.SQS;
using TesteAdmissional.Application.Dtos.SQS.Enum;
using TesteAdmissional.Application.Ports;

namespace TesteAdmissional.Application.CommandHandlers;

public class CreateContatoCommandHandler : IRequestHandler<CreateContatoCommand>
{
    private readonly ISqsFacade _sqsFacade;

    public CreateContatoCommandHandler(ISqsFacade sqsFacade)
    {
        _sqsFacade = sqsFacade;
    }

    public async Task Handle(CreateContatoCommand request, CancellationToken cancellationToken)
    {
        var body = JsonSerializer.Serialize(request);

        var message = new ContatoSqsMessage(MessageType.Create, body);

        await _sqsFacade.SendMessage(JsonSerializer.Serialize(message));
    }
}