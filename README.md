# Ajupov.Infrastructure.All

All packages for backend applications.

## Usage

1. Add nuget source: `nuget sources add -name GPR -Source https://nuget.pkg.github.com/ajupov`
2. Install package: `nuget install Ajupov.Infrastructure.All`

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
                    .AddSingleOriginCorsPolicy(configuration)
                    .AddMvc(typeof(SomeFilter))
                    .AddJwtAuthentication()
                    .AddJwtValidator(configuration)
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
                    .UseSingleOriginCors()
                    .UseAuthorization()
                    .UseMvcMiddleware();
            })
            .Build()
            .RunAsync();
    }
}

```

## Development
1. Clone this repository
2. Switch to a `new branch`
3. Make changes into `new branch`
4. Upgrade `PackageVersion` property value in `.csproj` file
5. Create pull request from `new branch` to `master` branch
6. Require code review
7. Merge pull request after approving
8. You can see package in [Github Packages](https://github.com/ajupov/Ajupov.Infrastructure.All/packages)
