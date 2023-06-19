using MediatR;
using TesteAdmissional.Domain.Aggregates.Enums;

namespace TesteAdmissional.Application.Commands;

public class UpdateContatoCommand : IRequest
{
    public UpdateContatoCommand(Guid id, string nome, string telefone, Sexo sexo, string cargo, DateOnly dataNascimento)
    {
        Id = id;
        Nome = nome;
        Telefone = telefone;
        Sexo = sexo;
        Cargo = cargo;
        DataNascimento = dataNascimento;
    }

    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Telefone { get; set; }
    public DateOnly DataNascimento { get; set; }
    public Sexo Sexo { get; set; }
    public string Cargo { get; set; }
}