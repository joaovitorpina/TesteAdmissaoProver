using Microsoft.Extensions.DependencyInjection;
using TesteAdmissional.Infra.Data;

namespace TesteAdmissional.Infra.Crosscutting.IoC.Configuration.Database;

public static class DatabaseSetup
{
    public static void AddDatabase(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<TesteAdmissionalContext>();
    }
}