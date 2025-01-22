using CrmIrExample.Command.Reports.ReportResponses;
using MediatR;

namespace CrmIrExample.Command.Reports.Interfaces;

public interface IReportingRequest<T> : IRequest<ReportAPIResponse<T>>
{
}