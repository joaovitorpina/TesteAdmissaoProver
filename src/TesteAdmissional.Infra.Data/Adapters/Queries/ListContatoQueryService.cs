using Microsoft.EntityFrameworkCore;
using TesteAdmissional.Application.Ports;
using TesteAdmissional.Domain.Aggregates;

namespace TesteAdmissional.Infra.Data.Adapters.Queries;

public class ListContatoQueryService : IListContatoQueryService
{
    private readonly TesteAdmissionalContext _context;

    public ListContatoQueryService(TesteAdmissionalContext context)
    {
        _context = context;
    }

    public async Task<List<Contato>> ListarContatos()
    {
        var contatos = await _context.Contatos.ToListAsync();

        return contatos;
    }
}