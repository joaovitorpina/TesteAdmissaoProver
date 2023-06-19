using TesteAdmissional.Domain.Aggregates.Enums;

namespace TesteAdmissional.Api.Controllers.Dtos;

public class UpdateContatoRequest
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Telefone { get; set; }
    public DateOnly DataNascimento { get; set; }
    public Sexo Sexo { get; set; }
    public string Cargo { get; set; }
}