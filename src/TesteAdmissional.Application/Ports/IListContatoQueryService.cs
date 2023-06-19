using TesteAdmissional.Domain.Aggregates;

namespace TesteAdmissional.Application.Ports;

public interface IListContatoQueryService
{
    public Task<List<Contato>> ListarContatos();
}