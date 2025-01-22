using CrmIrExample.HttpProxy;
using CrmIrExample.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Configure services
ConfigureServices(builder.Services);

var app = builder.Build();

// Configure middleware
ConfigureMiddleware(app);

app.Run();

void ConfigureServices(IServiceCollection services)
{
    var apiKey = builder.Configuration.GetSection("ApiKey").Value;
    var apiUrl = builder.Configuration.GetSection("ApiEndpoint").Value;
    
    services.AddControllers().AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    });
    
    services
        .AddSingleton(new ServiceProxyProvider<IInteractionServiceProxy>(apiUrl, apiKey)
            .GetServiceProxy())
        .AddSingleton(new ServiceProxyProvider<IReportingApiServiceProxy>(apiUrl, apiKey)
            .GetServiceProxy())
        .AddSingleton(new ServiceProxyProvider<ITicketServiceProxy>(apiUrl, apiKey)
            .GetServiceProxy())
        ;
    
    
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
    services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    services.AddTransient<ReportingServiceProxy>();
    services.AddTransient<CrmServiceProxy>();
    services.AddTransient<InteractionServiceProxy>();

}

void ConfigureMiddleware(WebApplication app)
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseAuthorization();

    var option = new RewriteOptions();
    option.AddRedirect("^$", "/swagger");
    app.UseRewriter(option);

    app.MapControllers();
}