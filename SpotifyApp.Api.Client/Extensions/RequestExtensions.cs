using Flurl.Http;
using SpotifyApp.Api.Contracts.Base.Requests;

namespace SpotifyApp.Api.Client.Extensions;

public static class RequestExtensions
{
    public static IFlurlRequest SetPagedQueryParams(this IFlurlRequest flurlRequest, PagedRequest request)
    {
        if (request.Limit != null) 
        {
            flurlRequest = flurlRequest.SetQueryParam("limit", request.Limit);
        }
        
        if (request.Offset != null)
        {
            flurlRequest = flurlRequest.SetQueryParam("offset", request.Offset);
        }
        
        return flurlRequest;
    }
}