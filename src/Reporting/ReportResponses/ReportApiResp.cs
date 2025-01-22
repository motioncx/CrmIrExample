using System.Collections.Generic;
using CrmIrExample.Command.Reports.Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CrmIrExample.Command.Reports.ReportResponses;

public class ReportApiResp
{
    [JsonProperty("resultCode")]
    public int ResultCode { get; set; }
    [JsonProperty("resultCodeName")]
    public string ResultCodeName { get; set; }
    [JsonProperty("data")]
    public IEnumerable<JObject> Data { get; set; }
    [JsonProperty("dataType")]
    public string DataType { get; set; }
    [JsonProperty("hasPaging")]
    public string HasPaging { get; set; }

    public ReportApiResp()
    {
        
    }
}