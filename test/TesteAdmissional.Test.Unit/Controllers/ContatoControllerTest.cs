using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NSubstitute;
using TesteAdmissional.Api.Controllers;
using TesteAdmissional.Api.Controllers.Dtos;
using TesteAdmissional.Application.CommandHandlers;
using TesteAdmissional.Application.Commands;
using TesteAdmissional.Application.Queries;
using TesteAdmissional.Domain.Aggregates;
using TesteAdmissional.Domain.Aggregates.Enums;

namespace TesteAdmissional.Test.Unit.Controllers;

public class ContatoControllerTest
{
    [Fact]
    public async Task CreateContato_DeveRetornar_Sucesso_Quando_CommandHandlerRetornar_SemErros()
    {
        var mediator = new Mock<IMediator>();
        var request = new CreateContatoRequest
        {
            Cargo = "Teste Cargo",
            DataNascimento = DateOnly.FromDateTime(DateTime.Today),
            Nome = "Teste Nome",
            Sexo = Sexo.Masculino,
            Telefone = "199999999"
        };

        mediator.Setup(m => m.Send(It.IsAny<CreateContatoCommand>(), new CancellationToken())).Verifiable();

        var controller = new ContatoController(mediator.Object);

        var result = await controller.Create(request);

        Assert.IsAssignableFrom<OkObjectResult>(result);
        var okResult = result as OkObjectResult;
        
        Assert.Equal(200, okResult!.StatusCode);
    }

    [Fact]
    public async Task CreateContato_DeveRetornar_BadRequest_Quando_CommandHandler_RetornarExcecao()
    {
        var mediator = new Mock<IMediator>();
        var request = new CreateContatoRequest
        {
            Cargo = "Teste Cargo",
            DataNascimento = DateOnly.FromDateTime(DateTime.Today),
            Nome = "Teste Nome",
            Sexo = Sexo.Masculino,
            Telefone = "199999999"
        };
        
        mediator.Setup(m => m.Send(It.IsAny<CreateContatoCommand>(), new CancellationToken())).Throws<Exception>();

        var controller = new ContatoController(mediator.Object);

        var result = await controller.Create(request);

        Assert.IsAssignableFrom<ObjectResult>(result);
        var okResult = result as ObjectResult;
        
        Assert.Equal(500, okResult!.StatusCode);
    }
    
    [Fact]
    public async Task UpdateContato_DeveRetornar_Sucesso_Quando_CommandHandlerRetornar_SemErros()
    {
        var mediator = new Mock<IMediator>();
        var request = new UpdateContatoRequest
        {
            Id = Guid.NewGuid(),
            Cargo = "Teste Cargo",
            DataNascimento = DateOnly.FromDateTime(DateTime.Today),
            Nome = "Teste Nome",
            Sexo = Sexo.Masculino,
            Telefone = "199999999"
        };

        mediator.Setup(m => m.Send(It.IsAny<UpdateContatoCommand>(), new CancellationToken())).Verifiable();

        var controller = new ContatoController(mediator.Object);

        var result = await controller.Update(request);

        Assert.IsAssignableFrom<OkObjectResult>(result);
        var okResult = result as OkObjectResult;
        
        Assert.Equal(200, okResult!.StatusCode);
    }

    [Fact]
    public async Task UpdateContato_DeveRetornar_BadRequest_Quando_CommandHandler_RetornarExcecao()
    {
        var mediator = new Mock<IMediator>();
        var request = new UpdateContatoRequest
        {
            Id = Guid.NewGuid(),
            Cargo = "Teste Cargo",
            DataNascimento = DateOnly.FromDateTime(DateTime.Today),
            Nome = "Teste Nome",
            Sexo = Sexo.Masculino,
            Telefone = "199999999"
        };
        
        mediator.Setup(m => m.Send(It.IsAny<UpdateContatoCommand>(), new CancellationToken())).Throws<Exception>();

        var controller = new ContatoController(mediator.Object);

        var result = await controller.Update(request);

        Assert.IsAssignableFrom<ObjectResult>(result);
        var okResult = result as ObjectResult;
        
        Assert.Equal(500, okResult!.StatusCode);
    }
    
    [Fact]
    public async Task DeleteContato_DeveRetornar_Sucesso_Quando_CommandHandlerRetornar_SemErros()
    {
        var mediator = new Mock<IMediator>();
        var request = new DeleteContatoRequest
        {
            Id = Guid.NewGuid()
        };

        mediator.Setup(m => m.Send(It.IsAny<DeleteContatoCommand>(), new CancellationToken())).Verifiable();

        var controller = new ContatoController(mediator.Object);

        var result = await controller.Delete(request);

        Assert.IsAssignableFrom<OkObjectResult>(result);
        var okResult = result as OkObjectResult;
        
        Assert.Equal(200, okResult!.StatusCode);
    }

    [Fact]
    public async Task DeleteContato_DeveRetornar_BadRequest_Quando_CommandHandler_RetornarExcecao()
    {
        var mediator = new Mock<IMediator>();
        var request = new DeleteContatoRequest
        {
            Id = Guid.NewGuid()
        };
        
        mediator.Setup(m => m.Send(It.IsAny<DeleteContatoCommand>(), new CancellationToken())).Throws<Exception>();

        var controller = new ContatoController(mediator.Object);

        var result = await controller.Delete(request);

        Assert.IsAssignableFrom<ObjectResult>(result);
        var okResult = result as ObjectResult;
        
        Assert.Equal(500, okResult!.StatusCode);
    }
    
    [Fact]
    public async Task ListContato_DeveRetornar_Sucesso_Quando_CommandHandlerRetornar_Contatos()
    {
        var mediator = new Mock<IMediator>();

        var contatos = new List<Contato>()
        {
            new("Teste 1", "112213123", new DateOnly(1999, 2, 8), Sexo.Masculino, new Cargo("Teste cargo")),
            new("Teste 2", "11111111", new DateOnly(2000, 2, 8), Sexo.Masculino, new Cargo("Teste cargo"))
        };

        mediator.Setup(m => m.Send(It.IsAny<ListContatoQuery>(), new CancellationToken())).Returns(
            Task.FromResult(contatos));

        var controller = new ContatoController(mediator.Object);

        var result = await controller.List();

        Assert.IsAssignableFrom<OkObjectResult>(result);
        var okResult = result as OkObjectResult;
        
        Assert.Equal(200, okResult!.StatusCode);

        Assert.IsAssignableFrom<SuccessResponse<List<Contato>>>(okResult.Value);
        var successResponse = okResult.Value as SuccessResponse<List<Contato>>;
        
        Assert.Equal(contatos.Count, successResponse?.Data?.Count);
    }

    [Fact]
    public async Task ListContato_DeveRetornar_BadRequest_Quando_CommandHandler_RetornarExcecao()
    {
        var mediator = new Mock<IMediator>();

        mediator.Setup(m => m.Send(It.IsAny<ListContatoQuery>(), new CancellationToken())).Throws<Exception>();

        var controller = new ContatoController(mediator.Object);

        var result = await controller.List();

        Assert.IsAssignableFrom<ObjectResult>(result);
        var okResult = result as ObjectResult;
        
        Assert.Equal(500, okResult!.StatusCode);
    }
}