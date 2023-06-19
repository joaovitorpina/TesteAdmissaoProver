using Elastic.Apm.NetCoreAll;
using Microsoft.OpenApi.Models;
using TesteAdmissional.Api.Authentication;
using TesteAdmissional.Infra.Crosscutting.IoC.Configuration;
using TesteAdmissional.Infra.Crosscutting.IoC.Configuration.Database;
using TesteAdmissional.Infra.Crosscutting.IoC.Configuration.EnvironmentVariables;
using TesteAdmissional.Infra.Crosscutting.IoC.Configuration.Swagger;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<TesteAdmissionalAuthKeyKeeper>();
builder.Services.AddAuthentication(options => { options.DefaultScheme = "TesteAdmissao"; })
    .AddScheme<TesteAdmissionalAuthSchemeOptions, TesteAdmissionalAuthHandler>("TesteAdmissao",
        null);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Description = "Autorizacão via Token usando Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Token" }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddEnvironmentVariables(builder.Configuration);
builder.Services.AddDatabase();
builder.Services.AddDependencyInjection();


var app = builder.Build();

app.AddSwagger("teste-admissao");

app.UseHttpsRedirection();
app.UseAllElasticApm(builder.Configuration);

app.UsePathBase(new PathString("/teste-admissao"));
app.UseRouting();

app.UseAuthentication()
    .UseAuthorization();

app.MapControllers();

app.Run();