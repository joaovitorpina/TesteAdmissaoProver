using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using TesteAdmissional.Api.Controllers.Dtos;

namespace TesteAdmissional.Api.Controllers;

public class ApiController : ControllerBase
{
    protected SuccessResponse<T> Success<T>(T? data)
    {
        return new SuccessResponse<T>(data);
    }

    protected ErrorResponse<T> Error<T>(T error)
    {
        return new ErrorResponse<T>(error);
    }

    protected (Guid, Guid) GetUserInfo()
    {
        var user = Guid.Parse(User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
        var companyId = Guid.Parse(User.Claims.First(c => c.Type == ClaimTypes.System).Value);

        return (user, companyId);
    }
}