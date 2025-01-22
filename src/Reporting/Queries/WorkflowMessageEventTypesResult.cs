using System.Collections.Generic;
using CrmIrExample.Command.Reports.Interfaces;

namespace CrmIrExample.Command.Reports.Queries;
public class WorkflowMessageEventTypesLookupQuery: IReportingRequest<WorkflowMessageEventTypesResult>
{
}
public class WorkflowMessageEventTypesResult {
    public Dictionary<int, string> EventTypes { get; set; }
}