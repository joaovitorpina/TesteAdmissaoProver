using MediatR;

namespace TesteAdmissional.Application.Commands;

public class DeleteContatoCommand : IRequest
{
    public DeleteContatoCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}