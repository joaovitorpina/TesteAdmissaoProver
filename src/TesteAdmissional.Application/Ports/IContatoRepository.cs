using TesteAdmissional.Domain.Aggregates;

namespace TesteAdmissional.Application.Ports;

public interface IContatoRepository
{
    public Task Create(Contato contato);
    public Task Update(Contato contato);
    public Task Delete(Guid id);
}