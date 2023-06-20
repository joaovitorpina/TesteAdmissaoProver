using MediatR;
using TesteAdmissional.Domain.Aggregates;

namespace TesteAdmissional.Application.Queries;

public class ListContatoQuery : IRequest<List<Contato>>
{
}