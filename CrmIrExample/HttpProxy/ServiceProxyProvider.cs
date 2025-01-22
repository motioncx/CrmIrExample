using CrmIrExample.Auth;
using Newtonsoft.Json;
using Refit;

namespace CrmIrExample.Services;
public class ServiceProxyProvider<T> where T : class
{
    private readonly string _url;
    private readonly string _apiKey;

    public ServiceProxyProvider(string url, string apiKey)
    {
        _url = url;
        _apiKey = apiKey;
    }

    public T GetServiceProxy()
    {
        var httpClient = new HttpClient(new AuthHandler(_apiKey));
        httpClient.BaseAddress = new Uri(_url);
        
        var serviceProxy = RestService.For<T>(httpClient, new RefitSettings
        {
            ContentSerializer = new NewtonsoftJsonContentSerializer(new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            })
            
        });
        
        
        
        return serviceProxy;
    }
}