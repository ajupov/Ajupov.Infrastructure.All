# Ajupov.Infrastructure.All

All packages for backend applications.

## Usage

```
public static class Program
{
    public static Task Main()
    {
        var configuration = ConfigurationExtensions.GetConfiguration();

        return configuration
            .ConfigureHost()
            .ConfigureLogging(configuration)
            .UseWebRoot(Directory.GetCurrentDirectory())
            .ConfigureServices((context, services) => services
                .ConfigureMvc(typeof(SomeFilter))
                .ConfigureJwt(configuration)
                .ConfigureTracing(configuration)
                .ConfigureApiDocumentation()
                .ConfigureMetrics(context.Configuration)
                .ConfigureMigrator(context.Configuration)
                .ConfigureMailSending(context.Configuration)
                .ConfigureSmsSending(context.Configuration)
                .ConfigureOrm<SomeStorage>(context.Configuration)
                .ConfigureHotStorage(context.Configuration)
                .AddTransient<ISomeService, Service>())
            .Configure((context, builder) =>
            {
                if (context.HostingEnvironment.IsDevelopment())
                {
                    builder.UseDeveloperExceptionPage();
                }

                builder.UseStaticFiles()
                    .UseApiDocumentationsMiddleware()
                    .UseMigrationsMiddleware()
                    .UseMetricsMiddleware()
                    .UseMvcMiddleware();
            })
            .Build()
            .RunAsync();
    }
}

```
