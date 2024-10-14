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

        AuthHandler authHandler = new AuthHandler(authToUse);
        var ticketService = RestService.For<ITicketServiceProxy>(new HttpClient(authHandler)
        {
            BaseAddress = new Uri(baseEndpoint)
        });


        var getTicketInfo = await ticketService.GetV1TicketAsync(new ReadTicketQueryCommand()
        {
            TicketId = (long)ticketId, TenantId = 3
        });

        if (getTicketInfo.IsSuccessStatusCode)
        {
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