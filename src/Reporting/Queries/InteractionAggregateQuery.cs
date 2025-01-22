using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace CrmIrExample.Command.Reports.Queries;

public class InteractionAggregateQuery : BaseQuery<InteractionAggregateFilter, IEnumerable<JObject>>
{
    
    public InteractionAggregateQuery( InteractionAggregateFilter request) : base(request)
    {
    }

}