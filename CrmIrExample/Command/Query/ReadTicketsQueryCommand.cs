using CrmIrExample.Models.CRM;
using MediatR;

namespace CrmIrExample.Command.Query;

public class ReadTicketsQuery : ReadTicketsModel, IRequest<TicketQueryResponse>
{
}
