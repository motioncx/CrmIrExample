﻿namespace CrmIrExample.Models.IR;

public class TranscriptResult
{
    public string ConversationId { get; set; }

    public string Title { get; set; }

    public List<ActivityDto> Messages { get; set; }
}