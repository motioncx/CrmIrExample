using CrmIrExample.Auth;
using CrmIrExample.Command.Reports.Queries;
using CrmIrExample.Command.Reports.ReportResponses;
using CrmIrExample.Controllers;
using CrmIrExample.HttpProxy;
using Newtonsoft.Json;
using Refit;
using Shared.Models.CRM;
using Shared.Models.IR;

namespace CrmIrExample.Services;

public class InteractionServiceProxy : BaseServiceProxy
{


    private ILogger<InteractionServiceProxy> _logger;
    private IHttpContextAccessor _httpContext;

    private IInteractionServiceProxy _interactionServiceProxy;


    public InteractionServiceProxy(
        IInteractionServiceProxy interactionServiceProxy, 
        IHttpContextAccessor httpContext, 
        ILogger<InteractionServiceProxy> logger) 
        : base(httpContext, logger)
    {
        _interactionServiceProxy = interactionServiceProxy;
        _logger = logger;
        _httpContext = httpContext;

    }

    public async Task<TranscriptResult?>  GetTranscript(Guid interactionId)
    {
        
        var resp = await _interactionServiceProxy.TranscriptsAsync(interactionId, false);
        if (!resp.IsSuccessStatusCode)
        {
            var errorMessage = "Unable to process [{InteractionId}]:" + interactionId + ", as failed to retrieve transcript!";
            LogInfo(errorMessage, LogLevel.Error);
            throw new Exception(errorMessage);
        }

        var transcriptResult = resp.Content;
        
        return transcriptResult;

    }

}