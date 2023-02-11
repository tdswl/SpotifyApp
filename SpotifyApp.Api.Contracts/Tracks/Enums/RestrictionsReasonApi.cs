using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SpotifyApp.Api.Contracts.Tracks.Enums;

[JsonConverter(typeof(StringEnumConverter))]
public enum RestrictionsReasonApi : byte
{
    [EnumMember(Value = "market")]
    Market = 0,
    
    [EnumMember(Value = "product")]
    Product = 1,
    
    [EnumMember(Value = "explicit")]
    Explicit = 2,
}