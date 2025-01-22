using Newtonsoft.Json.Linq;

namespace CrmIrExample.Command.Reports.Interfaces;

public interface IFilteredQuery
{
    public JObject Filter { get; }
}