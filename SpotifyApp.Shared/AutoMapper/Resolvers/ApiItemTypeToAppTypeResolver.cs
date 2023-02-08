using AutoMapper;
using SpotifyApp.Api.Contracts.Users.Models;
using SpotifyApp.Shared.Enums;
using SpotifyApp.Shared.Models;

namespace SpotifyApp.Shared.AutoMapper.Resolvers;

public sealed class ApiItemTypeToAppTypeResolver: IValueResolver<TopItemModel, ItemModel, ItemType>
{
    public ItemType Resolve(TopItemModel source, 
        ItemModel destination, 
        ItemType member, 
        ResolutionContext context)
    {
        return source.Type switch
        {
            "artist" => ItemType.Artist,
            "track" => ItemType.Track,
            _ => throw new ArgumentOutOfRangeException(nameof(source.Type), source.Type, "Wrong TopItemModel Type")
        };
    }
}