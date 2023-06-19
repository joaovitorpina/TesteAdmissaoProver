using HumanResourcesService.Environment;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TesteAdmissional.Infra.Environment.Configurations;

namespace TesteAdmissional.Infra.Crosscutting.IoC.Configuration.EnvironmentVariables;

public static class EnvironmentVariablesSetup
{
    public static void AddEnvironmentVariables(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        // serviceCollection.Configure<AmazonSQSConfig>(configuration.GetSection(EnvironmentConstants.AmazonSQSConfigName));
        // serviceCollection.Configure<DatabaseConfig>(
        //     configuration.GetSection(EnvironmentConstants.DatabaseConfigName));
    }
}