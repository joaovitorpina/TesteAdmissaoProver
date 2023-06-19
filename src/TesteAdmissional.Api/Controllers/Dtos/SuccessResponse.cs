namespace TesteAdmissional.Api.Controllers.Dtos;

public record SuccessResponse<T>(T? Data) : Response(true);