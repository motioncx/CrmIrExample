using MediatR;

namespace CrmIrExample.Command.Query;

public class ReadAttachmentsQueryCommand : IRequest<List<FileListItem>>, IBaseRequest
{
    public int TenantId { get; set; }

    public long TicketId { get; set; }
}