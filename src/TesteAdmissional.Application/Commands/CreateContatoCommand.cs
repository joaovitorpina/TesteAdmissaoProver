using MediatR;
using TesteAdmissional.Domain.Aggregates.Enums;

namespace TesteAdmissional.Application.Commands;

public class CreateContatoCommand : IRequest
{
    public CreateContatoCommand(string nome, string telefone, DateOnly dataNascimento, Sexo sexo, string cargo)
    {
        Nome = nome;
        Telefone = telefone;
        DataNascimento = dataNascimento;
        Sexo = sexo;
        Cargo = cargo;
    }

    public string Nome { get; set; }
    public string Telefone { get; set; }
    public DateOnly DataNascimento { get; set; }
    public Sexo Sexo { get; set; }
    public string Cargo { get; set; }
}