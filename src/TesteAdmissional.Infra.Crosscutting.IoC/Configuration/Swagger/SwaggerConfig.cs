using Microsoft.AspNetCore.Builder;
using Microsoft.OpenApi.Models;

namespace TesteAdmissional.Infra.Crosscutting.IoC.Configuration.Swagger;

public static class SwaggerConfig
{
    public static IApplicationBuilder AddSwagger(this IApplicationBuilder applicationBuilder, string basePath)
    {
        applicationBuilder.UseSwagger(swagger =>
        {
            swagger.RouteTemplate = basePath + "/swagger/{documentName}/swagger.json";
            swagger.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
            {
                swaggerDoc.Servers = new List<OpenApiServer>
                    { new() { Url = $"https://{httpReq.Host.Value}/{basePath}" } };
            });
        });

        applicationBuilder.UseSwaggerUI(swaggerOptions =>
        {
            swaggerOptions.SwaggerEndpoint($"/{basePath}/swagger/v1/swagger.json", "Profissionais Service");
            swaggerOptions.RoutePrefix = $"{basePath}/swagger";
        });

        return applicationBuilder;
    }
}