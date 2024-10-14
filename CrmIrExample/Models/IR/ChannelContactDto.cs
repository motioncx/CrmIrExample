using CrmIrExample.Models.CRM;

namespace CrmIrExample.Models.IR;

public class ChannelContactDto : ContactDto
{
    public string ChannelParticipantId { get; set; }

    public bool? Verified { get; set; }
}