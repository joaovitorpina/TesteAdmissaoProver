using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using TesteAdmissional.Api.Authentication;
using TesteAdmissional.Api.Controllers;
using TesteAdmissional.Api.Controllers.Dtos;
using TesteAdmissional.Infra.Environment.Configurations;

namespace TesteAdmissional.Test.Unit.Controllers;

public class LoginControllerTest
{
    [Fact]
    public void Login_Deve_Retornar_Sucesso_Quando_UserEPassword_EstiveremCorretos()
    {
        var loginConfig = new Mock<IOptions<LoginConfig>>();
        var keyKeeper = new Mock<ITesteAdmissionalAuthKeyKeeper>();
        var token = "aa3mr30rk30mfQ";

        loginConfig.Setup(l => l.Value).Returns(new LoginConfig { User = "usuario", Password = "senha" });
        keyKeeper.Setup(k => k.CreateNewToken()).Returns(token);

        var loginController = new LoginController(loginConfig.Object, keyKeeper.Object);

        var request = new LoginRequest("usuario", "senha");

        var result = loginController.Login(request);

        Assert.IsAssignableFrom<OkObjectResult>(result);
        var okResult = result as OkObjectResult;

        Assert.IsAssignableFrom<SuccessResponse<string>>(okResult?.Value);
        var successResult = okResult?.Value as SuccessResponse<string>;

        Assert.Equal(token, successResult?.Data);
    }

    [Fact]
    public void Login_Deve_Retornar_BadRequest_Quando_UserOuPassword_EstiveremIncorretos()
    {
        var loginConfig = new Mock<IOptions<LoginConfig>>();
        var keyKeeper = new Mock<ITesteAdmissionalAuthKeyKeeper>();
        var token = "aa3mr30rk30mfQ";

        loginConfig.Setup(l => l.Value).Returns(new LoginConfig { User = "usuario1", Password = "senha2" });
        keyKeeper.Setup(k => k.CreateNewToken()).Returns(token);

        var loginController = new LoginController(loginConfig.Object, keyKeeper.Object);

        var request = new LoginRequest("usuario", "senha");

        var result = loginController.Login(request);

        Assert.IsAssignableFrom<ObjectResult>(result);
        var errorResult = result as ObjectResult;

        Assert.Equal(400, errorResult?.StatusCode);
    }
    
    [Fact]
    public void Login_Deve_Retornar_InternalServerError_Quando_KeyKeeper_Retornar_Excecao()
    {
        var loginConfig = new Mock<IOptions<LoginConfig>>();
        var keyKeeper = new Mock<ITesteAdmissionalAuthKeyKeeper>();

        loginConfig.Setup(l => l.Value).Returns(new LoginConfig { User = "usuario", Password = "senha" });
        keyKeeper.Setup(k => k.CreateNewToken()).Throws<Exception>();

        var loginController = new LoginController(loginConfig.Object, keyKeeper.Object);

        var request = new LoginRequest("usuario", "senha");

        var result = loginController.Login(request);

        Assert.IsAssignableFrom<ObjectResult>(result);
        var errorResult = result as ObjectResult;

        Assert.Equal(500, errorResult?.StatusCode);
    }
}