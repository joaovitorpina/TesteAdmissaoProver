namespace TesteAdmissional.Domain.Exceptions;

public class DataNascimentoInvalidaException : Exception
{
    public DataNascimentoInvalidaException(DateOnly data) : base(
        $"Data {data.ToString("d")} é igual ou menor que o dia atual")
    {
    }
}