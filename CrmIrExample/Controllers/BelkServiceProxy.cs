using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CrmIrExample.Services;

public class BelkServiceProxy
{

    // just copying/code here... obviously, appsettings or somewhere else than static example
    private static string _belkUatApiKey = "PUT_KEY_HERE";
    private static string _belkUatClientUserId = "PUT_USER_HERE";
    private static string _belkProdApiKey = "PUT_KEY_HERE";
    private static string _belkProdClientUserId = "PUT_USER_HERE";
    
    

    public static async Task<JObject> GetOrdersByCustomerId(string customerId, int limit, string env)
    {
        
        
        
        var client_ = new System.Net.Http.HttpClient();
        try
        {
            using (var request_ = new System.Net.Http.HttpRequestMessage())
            {
                var cackeKey = "BelkOrderDetailSearch_QA_";
                var url_ = "";
                request_.Method = new System.Net.Http.HttpMethod("GET");
                request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));
                if(env.ToLower() == "prod")
                {
                    request_.Headers.Add("apiKey", _belkProdApiKey);
                    request_.Headers.Add("client", _belkProdClientUserId);
                    url_ = $"https://apps.belk.com/v1/orders?customerId={customerId}&offset=0&limit={limit}";
                     cackeKey = "BelkOrderDetailSearch_PROD_";
                }
                else
                {
                    request_.Headers.Add("apiKey", _belkUatApiKey);
                    request_.Headers.Add("client", _belkUatClientUserId);
                    url_ = $"https://apps-qa.belk.com/v1/orders?customerId={customerId}&offset=0&limit={limit}";
                    
                }
                
                request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);
                cackeKey += customerId != null ? customerId : "";

                string responseJson = null;
                try
                {
                    var response = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, default(CancellationToken)).ConfigureAwait(false);
                    
                    var headers_ = System.Linq.Enumerable.ToDictionary(response.Headers, h_ => h_.Key, h_ => h_.Value);
                    if (response.Content != null && response.Content.Headers != null)
                    {
                        foreach (var item_ in response.Content.Headers)
                            headers_[item_.Key] = item_.Value;
                    }
                    var status_ = (int)response.StatusCode;
                    if (status_ == 200)
                    {
                        if (response.Content is object)// &&  response_.Content.Headers.ContentType.MediaType == "application/json")
                        {
                            var contentStream = await response.Content.ReadAsStreamAsync();
                            using var streamReader = new StreamReader(contentStream);
                            using var jsonReader = new JsonTextReader(streamReader);
                            try
                            {
                                var json = streamReader.ReadToEnd();
                                responseJson = json;
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Invalid JSON.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("HTTP Response was invalid and cannot be deserialised.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Exception while querying for order details", ex);
                } 

            }
        }
        finally
        {
        }
        return null;
    }
}
