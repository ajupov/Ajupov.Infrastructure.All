# Ajupov.Infrastructure.All

All packages for backend applications.

## Packaging

##### Pack
```dotnet pack --configuration Release```

##### Push
```nuget push ".\bin\Release\Ajupov.Infrastructure.All.{version}.nupkg" -Source "GitHub"```

## Configure

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
