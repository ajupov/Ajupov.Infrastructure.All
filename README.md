# Ajupov.Infrastructure.All
All packages for backend applications

## Usage
1. Add nuget source: `nuget sources add -name GPR -Source https://nuget.pkg.github.com/ajupov`
2. Install package: `nuget install Ajupov.Infrastructure.All`
3. Configure `Startup.cs`
```
public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services
            .AddAuthorization()
            .AddJwtAuthentication()
            .AddJwtValidator(configuration)
            .AddCookieDefaults();

        services
            .AddCookiePolicy()
            .AddSingleOriginCorsPolicy(configuration)
            .AddControllers(typeof(SomeFilter))
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
    }
    
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage()
                .UseForwardedHeaders()
                .UseHttpsRedirection()
                .UseHsts();
        }

        app.UseStaticFiles()
            .UseApiDocumentationsMiddleware()
            .UseMigrationsMiddleware()
            .UseMetricsMiddleware()
            .UseSingleOriginCors()
            .UseAuthentication()
            .UseRouting()
            .UseAuthorization()
            .UseControllers();
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
