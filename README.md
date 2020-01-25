# Ajupov.Infrastructure.All

All packages for backend applications.

## Usage

```
public static class Program
{
    public static Task Main()
    {
        var configuration = Configuration.GetConfiguration();

        return configuration
            .ConfigureHosting()
            .ConfigureWebRoot()
            .ConfigureLogging(configuration)
            .UseWebRoot(Directory.GetCurrentDirectory())
            .ConfigureServices((context, services) =>
            {
                services
                    .AddMvc(typeof(SomeFilter))
                    .AddJwtGenerator()
                    .AddJwtReader()
                    .AddTracing(configuration)
                    .AddApiDocumentation()
                    .AddMetrics(context.Configuration)
                    .AddMigrator(context.Configuration)
                    .AddMailSending(context.Configuration)
                    .AddSmsSending(context.Configuration)
                    .AddOrm<SomeStorage>(context.Configuration)
                    .AddHotStorage(context.Configuration);
                    
                services
                    .AddTransient<ISomeService, SomeService>());
            .Configure((context, builder) =>
            {
                if (context.HostingEnvironment.IsDevelopment())
                {
                    builder.UseDeveloperExceptionPage();
                }

                builder
                    .UseStaticFiles()
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
