using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TesteAdmissional.Api.Controllers.Dtos;
using TesteAdmissional.Application.Commands;
using TesteAdmissional.Application.QuerieHandlers;
using TesteAdmissional.Application.Queries;

namespace TesteAdmissional.Api.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize]
public class ContatoController : ApiController
{
    private readonly IMediator _mediator;

    public ContatoController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResponse<string>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse<string>))]
    public async Task<IActionResult> Create([FromBody] CreateContatoRequest request)
    {
        try
        {
            await _mediator.Send(new CreateContatoCommand(request.Nome, request.Telefone, request.DataNascimento, request.Sexo, request.Cargo));

            return Ok(Success("Contato criado com sucesso!"));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, Error(e.Message));
        }
    }

    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResponse<string>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse<string>))]
    public async Task<IActionResult> Update([FromBody] UpdateContatoRequest request)
    {
        try
        {
            await _mediator.Send(new UpdateContatoCommand(request.Id, request.Nome, request.Telefone, request.Sexo, request.Cargo, request.DataNascimento));

            return Ok(Success("Contato atualizado com sucesso!"));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, Error(e.Message));
        }
    }
    
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResponse<string>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse<string>))]
    public async Task<IActionResult> Delete([FromBody] DeleteContatoRequest request)
    {
        try
        {
            await _mediator.Send(new DeleteContatoCommand(request.Id));

            return Ok(Success("Contato deletado com sucesso!"));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, Error(e.Message));
        }
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResponse<string>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse<string>))]
    public async Task<IActionResult> List()
    {
        try
        {
            var result = await _mediator.Send(new ListContatoQuery());

            return Ok(Success(result));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, Error(e.Message));
        }
    }
}