using CrmIrExample.Command.Reports.Interfaces;

namespace CrmIrExample.Command.Reports;

public abstract class BaseQuery<TQuery, TResponse>: IReportingRequest<TResponse>
{
    #region Properties
        
    
    public TQuery Request { get; }

    #endregion

    #region Constructors/Destructors
        
    protected BaseQuery( TQuery request)
    {
        
        Request = request;
    }

    #endregion
}