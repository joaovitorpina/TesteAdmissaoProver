using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TesteAdmissional.Api.Authentication;
using TesteAdmissional.Api.Controllers.Dtos;
using TesteAdmissional.Infra.Environment.Configurations;

namespace TesteAdmissional.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class LoginController : ApiController
{
    private readonly IOptions<LoginConfig> _loginConfig;
    private readonly TesteAdmissionalAuthKeyKeeper _keyKeeper;

    public LoginController(IOptions<LoginConfig> loginConfig, TesteAdmissionalAuthKeyKeeper keyKeeper)
    {
        _loginConfig = loginConfig;
        _keyKeeper = keyKeeper;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResponse<string>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse<string>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse<string>))]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        try
        {
            if (request.User != _loginConfig.Value.User || request.Password != _loginConfig.Value.Password)
            {
                return BadRequest(Error("Usuario ou senha estao invalidos"));
            }

            var token = _keyKeeper.CreateNewToken();

            return Ok(Success(token));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, Error(e.Message));
        }
    }
    
    [HttpPost("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResponse<string>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse<string>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse<string>))]
    public IActionResult GetTokenPasswordRecovery([FromBody] GetTokenPasswordRecovery request)
    {
        try
        {
            if (request.User != _loginConfig.Value.User)
            {
                return BadRequest(Error("Usuario nao existente"));
            }

            var token = _keyKeeper.CreateTokenPasswordRecovery();

            return Ok(Success(token));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, Error(e.Message));
        }
    }
    
    [HttpPost("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResponse<string>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse<string>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse<string>))]
    public IActionResult RecoverPassword([FromBody] RecoverPasswordRequest request)
    {
        try
        {
            if (_keyKeeper.CheckTokenIsValid(request.Token))
            {
                return BadRequest(Error("Token invalido"));
            }
            
            return Ok(Success(string.Empty));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, Error(e.Message));
        }
    }
}