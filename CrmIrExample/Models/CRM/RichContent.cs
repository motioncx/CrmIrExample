namespace CrmIrExample.Models.CRM;

public class RichContent
{
    public string OriginalContent { get; set; }

    public string ReplacedContent { get; set; }

    public List<RichFile> Files { get; set; }
}