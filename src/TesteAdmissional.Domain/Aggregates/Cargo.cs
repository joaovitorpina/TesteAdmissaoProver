using TesteAdmissional.Domain.SeedWork;

namespace TesteAdmissional.Domain.Aggregates;

public class Cargo : ValueObject
{
    public Cargo(string descricao)
    {
        Descricao = descricao;
        Ativo = true;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Descricao;
        yield return Ativo;
    }
    
    public string Descricao { get; }
    public bool Ativo { get; set; }
}