using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TesteAdmissional.Application.Ports;
using TesteAdmissional.Infra.Bus.SQS;
using TesteAdmissional.Infra.Data.Adapters.Queries;
using TesteAdmissional.Infra.Data.Adapters.Repositories;
using TesteAdmissional.Infra.Facade;

namespace TesteAdmissional.Infra.Crosscutting.IoC;

public static class NativeInjectorBootstrapper
{
    public static void RegisterServices(IServiceCollection serviceCollection)
    {
        RegisterMediatR(serviceCollection);
        RegisterQueries(serviceCollection);
        RegisterRepositories(serviceCollection);
        RegisterFacades(serviceCollection);
        RegisterProcessors(serviceCollection);
    }

    private static void RegisterMediatR(IServiceCollection serviceCollection)
    {
        const string applicationAssemblyName = "TesteAdmissional.Application";
        var assembly = AppDomain.CurrentDomain.Load(applicationAssemblyName);

        serviceCollection.AddMediatR(assembly);
    }

    private static void RegisterQueries(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IListContatoQueryService, ListContatoQueryService>();
    }

    private static void RegisterRepositories(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IContatoRepository, ContatoRepository>();
    }

    private static void RegisterFacades(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<ISqsFacade, SqsFacade>();
    }

    private static void RegisterProcessors(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<ContatoProcessor>();
    }
}