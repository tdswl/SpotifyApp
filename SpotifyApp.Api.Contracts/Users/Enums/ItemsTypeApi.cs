using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SpotifyApp.Api.Contracts.Users.Enums;

[JsonConverter(typeof(StringEnumConverter))]
public enum ItemsTypeApi : byte
{
    [EnumMember(Value = "artist")]
    Artist = 0,
    
    [EnumMember(Value = "track")]
    Track = 1,
    
    [EnumMember(Value = "user")]
    User = 2,
    
    [EnumMember(Value = "album")]
    Album = 3,
    
    [EnumMember(Value = "audio_features")]
    AudioFeatures = 4,
    
    [EnumMember(Value = "genre")]
    Genre = 5,
}