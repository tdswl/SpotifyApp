using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SpotifyApp.Api.Contracts.Tracks.Enums;

[JsonConverter(typeof(StringEnumConverter))]
public enum ReleaseDatePrecisionApi : byte
{
    [EnumMember(Value = "year")]
    Year = 0,
    
    [EnumMember(Value = "month")]
    Month = 1,
    
    [EnumMember(Value = "day")]
    Day = 2,
}