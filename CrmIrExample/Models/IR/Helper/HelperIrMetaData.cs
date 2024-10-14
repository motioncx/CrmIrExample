using Newtonsoft.Json;

namespace CrmIrExample.Models.IR.Helper;

public class HelperIrMetaData
{
    public static InteractionMetaData? DeserializeInteractionMetaData(string interactionMetaData)
    {
        try
        {
            if (!string.IsNullOrWhiteSpace(interactionMetaData))
            {
                InteractionMetaData metaData = JsonConvert.DeserializeObject<InteractionMetaData>(interactionMetaData);
                return metaData;
            }

        }
        catch (Exception ex)
        {

         
        }
        return null;
    }
}