using Newtonsoft.Json;
using SpotifyApp.Api.Contracts.Users.Models;

namespace SpotifyApp.Api.Contracts.Users.Responses;

public sealed class GetUsersTopItemsResponse
{
    [JsonProperty("href")]
    public string Href { get; set; }
    
    [JsonProperty("limit")]
    public int Limit { get; set; }
    
    [JsonProperty("next")]
    public string Next { get; set; }
    
    [JsonProperty("offset")]
    public int Offset { get; set; }
    
    [JsonProperty("previous")]
    public string Previous { get; set; }
    
    [JsonProperty("total")]
    public int Total { get; set; }
    
    [JsonProperty("items")]
    public List<TopItemModel> Items { get; set; }
    
}