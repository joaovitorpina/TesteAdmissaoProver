using MediatR;
using TesteAdmissional.Application.Ports;
using TesteAdmissional.Application.Queries;
using TesteAdmissional.Domain.Aggregates;

namespace TesteAdmissional.Application.QuerieHandlers;

public class ListContatoQueryHandler : IRequestHandler<ListContatoQuery, List<Contato>>
{
    private readonly IListContatoQueryService _listContatoQueryService;

    public ListContatoQueryHandler(IListContatoQueryService listContatoQueryService)
    {
        _listContatoQueryService = listContatoQueryService;
    }

    public async Task<List<Contato>> Handle(ListContatoQuery request, CancellationToken cancellationToken)
    {
        var contatos = await _listContatoQueryService.ListarContatos();

        return contatos;
    }
}