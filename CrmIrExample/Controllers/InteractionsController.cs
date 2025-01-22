using System.Diagnostics;
using CrmIrExample.Auth;
using CrmIrExample.Command.Query;
using CrmIrExample.Command.Reports.Domain;
using CrmIrExample.Command.Reports.Filtering;
using CrmIrExample.Command.Reports.Queries;
using CrmIrExample.Command.Reports.ReportResponses;
using CrmIrExample.HttpProxy;
using CrmIrExample.Services;
using Shared.Models.IR.Helper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Refit;
using Shared.Models.CRM;

namespace CrmIrExample.Controllers;


[ApiController]
[Route("[controller]")]
public class InteractionsController(
        CrmServiceProxy crmServiceProxy, 
        InteractionServiceProxy interactionServiceProxy
        ): ControllerBase
{
    
    private InteractionServiceProxy _interactionServiceProxy = interactionServiceProxy;
    


    [HttpGet]
    [Route("get-tickets")]
    public async Task<IActionResult> GetTranscript(Guid interactionId)
    {
        var trasnscript = await _interactionServiceProxy.GetTranscript(interactionId);
        return Ok(trasnscript);
    }
    

}