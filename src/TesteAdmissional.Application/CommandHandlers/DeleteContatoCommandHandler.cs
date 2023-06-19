using System.Text.Json;
using MediatR;
using TesteAdmissional.Application.Commands;
using TesteAdmissional.Application.Dtos.SQS;
using TesteAdmissional.Application.Dtos.SQS.Enum;
using TesteAdmissional.Application.Ports;

namespace TesteAdmissional.Application.CommandHandlers;

public class DeleteContatoCommandHandler : IRequestHandler<DeleteContatoCommand>
{
    private readonly ISqsFacade _sqsFacade;

    public DeleteContatoCommandHandler(ISqsFacade sqsFacade)
    {
        _sqsFacade = sqsFacade;
    }

    public async Task Handle(DeleteContatoCommand request, CancellationToken cancellationToken)
    {
        var body = JsonSerializer.Serialize(request);

        var message = new ContatoSqsMessage(MessageType.Delete, body);

        await _sqsFacade.SendMessage(JsonSerializer.Serialize(message));
    }
}