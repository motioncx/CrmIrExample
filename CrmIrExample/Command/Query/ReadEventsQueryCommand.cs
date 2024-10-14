using CrmIrExample.Models;
using MediatR;

namespace CrmIrExample.Command.Query;

public class ReadEventsQueryCommand : 
    ReadListQuery,
    IRequest<ReadEventsQueryCommandResponse>,
    IBaseRequest
{
    public int TicketId { get; set; }
}