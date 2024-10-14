namespace CrmIrExample.Models;

public class PagedList<T>
{
    public long TotalCount { get; set; }

    public IEnumerable<T> Items { get; set; }
}