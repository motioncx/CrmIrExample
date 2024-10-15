using CrmIrExample.Auth;
using CrmIrExample.Command.Query;
using CrmIrExample.HttpProxy;
using CrmIrExample.Models.IR.Helper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Refit;

namespace CrmIrExample.Controllers;


[ApiController]
[Route("[controller]")]
public class TicketRetrieveController : ControllerBase
{

    private const string _apiKeyProd =
        "hViGEaqo6ov3WMds2zvwqmHU6g65n7Foa0+aFjZnlZg=:1kdbZWeRNZiEkG53LtaVTY3dBx8o1BK8pfVI0I9+5j8=";

    private ILogger<TicketRetrieveController> _logger;

    public TicketRetrieveController(ILogger<TicketRetrieveController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Get(long ticketId)
    {
        var authToUse = _apiKeyProd;
        var baseEndpoint = "https://api.motioncxapps.com";

        // add auth handler (can likely move this and the refit to a service extension and add as DI
        AuthHandler authHandler = new AuthHandler(authToUse);
        // create refit logic to get ticketserviceproxy proxy
        var ticketService = RestService.For<ITicketServiceProxy>(new HttpClient(authHandler)
        {
            BaseAddress = new Uri(baseEndpoint)
        });

        // if you want to get details on ticket(s)
        var getTicketsTest = await ticketService.GetVTicketsAsync(new ReadTicketsQuery()
        {
            TenantId = 3, CreatedAt = new DateTime(2024, 10, 11), CreatedTo = new DateTime(2024, 10, 15), MaxResultCount = 25, SkipCount = 0,
            QueueIds = [1, 2, 3]
        });

        
        // call specific ticket id
        var getTicketInfo = await ticketService.GetV1TicketAsync(new ReadTicketQueryCommand()
        {
            TicketId = (long)ticketId, TenantId = 3
        });

        // check if success
        if (getTicketInfo.IsSuccessStatusCode)
        {
            // get details to get interactionid
            var ticketInfo = getTicketInfo.Content;
            var contactInfo = ticketInfo.Contact;

            var getInfo = ticketInfo.IRWorkItemInfoDetails;
            var irMetaData = HelperIrMetaData.DeserializeInteractionMetaData(getInfo);
            if (irMetaData == null)
            {
                var errorMessage = "Unable to process [{TicketId}]:" + ticketId + ", as there is no IRMetaData!";
                _logger.LogError(errorMessage);
                throw new Exception(errorMessage);
            }
            
            // create refit logic to get interacitonservice proxy
            var interactionServiceProxy = RestService.For<IInteractionServiceProxy>(new HttpClient(authHandler)
            {
                BaseAddress = new Uri(baseEndpoint)
            });
            var resp = await interactionServiceProxy.TranscriptsAsync(irMetaData.InteractionId, false);
            if (!resp.IsSuccessStatusCode)
            {
                var errorMessage = "Unable to process [{TicketId}]:" + ticketId + ", as failed to retrieve transcript!";
                _logger.LogError(errorMessage);
                throw new Exception(errorMessage);
            }

            var transcriptResult = resp.Content;
            var toDeserialized = JsonConvert.SerializeObject(transcriptResult);

        }

        return Ok();
    }

}