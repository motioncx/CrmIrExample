using System.Collections.Generic;

namespace CrmIrExample.Command.Reports.Domain;

public class PagedResult<T>
{
    public PageDetails Paging { get; set; }
    public IReadOnlyList<T> data;        
}
    