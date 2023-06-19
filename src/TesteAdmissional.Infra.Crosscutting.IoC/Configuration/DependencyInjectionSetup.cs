using Microsoft.Extensions.DependencyInjection;

namespace TesteAdmissional.Infra.Crosscutting.IoC.Configuration;

public static class DependencyInjectionSetup
{
    public static void AddDependencyInjection(this IServiceCollection services)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        NativeInjectorBootstrapper.RegisterServices(services);
    }
}