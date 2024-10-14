using System.Net.Http.Headers;

namespace CrmIrExample.Auth;



public class AuthHandler : HttpClientHandler
{
    private readonly string _apiKey;
    private AuthHandler _authHander;

    public AuthHandler(string apiKey)
    {
        _apiKey = apiKey;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        // setup to call apikey
        request.Headers.Add("ApiKey", _apiKey);
        // add authorization too, just in case
        request.Headers.Authorization = new AuthenticationHeaderValue(
            "apiKey",
            _apiKey);

        return await base.SendAsync(request, cancellationToken)
            .ConfigureAwait(false);
    }
}