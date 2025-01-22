// using CrmIrExample.Auth;
// using CrmIrExample.HttpProxy;
// using Newtonsoft.Json;
// using Refit;
//
// namespace CrmIrExample;
//
// public static class AppConfiguration
// {
//
//     public static IServiceCollection SetupServices(IServiceCollection serviceCollection, 
//      string url, string apiKey) 
//     {
//         var _authHandler = new AuthHandler(apiKey);
//
//         var _refitSettings = new RefitSettings
//         {
//             AuthorizationHeaderValueGetter  =  (rq, ct)  => Task.FromResult("x-api-key " + apiKey),
//             ContentSerializer = new NewtonsoftJsonContentSerializer(new JsonSerializerSettings()
//             {
//                 ReferenceLoopHandling = ReferenceLoopHandling.Ignore
//             })
//
//         };
//             
//     
//         serviceCollection
//             .AddRefitClient<IInteractionServiceProxy>(_refitSettings)
//             .ConfigureHttpClient(c =>  c.BaseAddress = new Uri(url));
//         return serviceCollection;
//     }
//     
// }