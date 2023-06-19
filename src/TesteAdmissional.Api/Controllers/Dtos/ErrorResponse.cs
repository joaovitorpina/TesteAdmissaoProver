namespace TesteAdmissional.Api.Controllers.Dtos;

public record ErrorResponse<T>(T Error) : Response(false);