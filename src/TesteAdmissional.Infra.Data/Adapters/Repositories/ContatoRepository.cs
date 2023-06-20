using Microsoft.EntityFrameworkCore;
using TesteAdmissional.Application.Ports;
using TesteAdmissional.Domain.Aggregates;

namespace TesteAdmissional.Infra.Data.Adapters.Repositories;

public class ContatoRepository : IContatoRepository
{
    private readonly TesteAdmissionalContext _context;

    public ContatoRepository(TesteAdmissionalContext context)
    {
        _context = context;
    }


    public async Task Create(Contato contato)
    {
        _context.Contatos.Add(contato);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Contato contato)
    {
        _context.Contatos.Update(contato);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        var contato = await _context.Contatos.FirstOrDefaultAsync(c => c.Id == id);

        if (contato is null) return;

        _context.Contatos.Remove(contato);
        await _context.SaveChangesAsync();
    }
}