using CrmIrExample.Services;
using Microsoft.AspNetCore.Mvc;

namespace CrmIrExample.Controllers;


[ApiController]
[Route("[controller]")]
public class BelkApiController: ControllerBase
{
    [HttpGet]
    
    public async Task<IActionResult> GetTranscript(string customerId, string env, int limit = 10)
    {
         var resp = await BelkServiceProxy.GetOrdersByCustomerId(customerId, limit, env);
         return Ok(resp);
        
    }
}