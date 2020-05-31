using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit.DependencyInjection;
using Xunit.Sdk;

namespace Ajupov.Infrastructure.All.TestsDependencyInjection
{
    public class BaseStartup : DependencyInjectionTestFramework
    {
        public BaseStartup()
            : base(new NullMessageSink())
        {
        }

        protected void ConfigureServices(IServiceCollection services)
        {
            Configure(services);

            services.BuildServiceProvider();
        }

        protected override IHostBuilder CreateHostBuilder(AssemblyName assemblyName)
        {
            return base
                .CreateHostBuilder(assemblyName)
                .ConfigureServices(ConfigureServices);
        }

        protected virtual void Configure(IServiceCollection services)
        {
        }
    }
}
