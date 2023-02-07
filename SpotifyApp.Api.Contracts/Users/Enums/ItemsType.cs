using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SpotifyApp.Api.Contracts.Users.Enums;

[JsonConverter(typeof(StringEnumConverter))]
public enum ItemsType : byte
{
    [EnumMember(Value = "artists")]
    Artists = 0,
    
    [EnumMember(Value = "tracks")]
    Tracks = 1,
}