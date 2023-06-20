using TesteAdmissional.Domain.Aggregates.Enums;
using TesteAdmissional.Domain.Exceptions;
using TesteAdmissional.Domain.SeedWork;

namespace TesteAdmissional.Domain.Aggregates;

public class Contato : Entity, IAggregateRoot
{
    private DateOnly _dataNascimento;

    public Contato(string nome, string telefone, DateOnly dataNascimento, Sexo sexo, Cargo cargo, Guid? id = null,
        DateTime? createdAt = null, DateTime? updatedAt = null) : base(id, createdAt, updatedAt)
    {
        Nome = nome;
        Telefone = telefone;
        DataNascimento = dataNascimento;
        Sexo = sexo;
        Cargo = cargo;
    }

    public string Nome { get; set; }
    public string Telefone { get; set; }

    public DateOnly DataNascimento
    {
        get => _dataNascimento;
        set
        {
            if (!DataNascimentoIsValid(value)) throw new DataNascimentoInvalidaException(value);

            _dataNascimento = value;
            Idade = GerarIdade(value);
        }
    }

    public int Idade { get; set; }
    public Sexo Sexo { get; set; }
    public bool Ativo { get; set; }
    public Cargo Cargo { get; set; }

    private bool DataNascimentoIsValid(DateOnly data)
    {
        return data < DateOnly.FromDateTime(DateTime.Today);
    }

    private int GerarIdade(DateOnly dataNascimento)
    {
        var idade = DateTime.Now.Year - dataNascimento.Year;

        return idade;
    }
}