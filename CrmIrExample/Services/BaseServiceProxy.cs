using System.Diagnostics;
using CrmIrExample.Auth;
using CrmIrExample.Command.Reports.Queries;
using CrmIrExample.Command.Reports.ReportResponses;
using CrmIrExample.Controllers;
using CrmIrExample.HttpProxy;
using Newtonsoft.Json;
using Refit;

namespace CrmIrExample.Services;

public partial class BaseServiceProxy
{
    protected string? _apiKeyProd =
        "";

    private ILogger<BaseServiceProxy> _logger;
    private IHttpContextAccessor _httpContext;

    protected void LogInfo(string log, LogLevel logLevel = LogLevel.Debug, Exception ex = null)
    {
        // dont feel like copy/pasting logging wrapper from mcx logger, just show log and console
        var traceId = Activity.Current?.Id ?? _httpContext?.HttpContext?.TraceIdentifier;
        Console.WriteLine(log);
        _logger.Log(logLevel,ex,  log, new object?[] { traceId});
        
    }

    public BaseServiceProxy(IHttpContextAccessor httpContext, ILogger<BaseServiceProxy> logger)
    {
      
        _logger = logger;
        _httpContext = httpContext;
       
    }


}