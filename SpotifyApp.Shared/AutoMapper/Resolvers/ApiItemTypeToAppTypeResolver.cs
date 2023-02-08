using AutoMapper;
using SpotifyApp.Api.Contracts.Users.Models;
using SpotifyApp.Shared.Enums;
using SpotifyApp.Shared.Models;

namespace SpotifyApp.Shared.AutoMapper.Resolvers;

public sealed class ApiItemTypeToAppTypeResolver: IValueResolver<TopItemModel, ItemWithImages, ItemType>
{
    public ItemType Resolve(TopItemModel source, 
        ItemWithImages destination, 
        ItemType member, 
        ResolutionContext context)
    {
        switch (source.Type)
        {
            case "artist":
                return ItemType.Artist;
            case "album":
                return ItemType.Album;
            default:
                throw new ArgumentOutOfRangeException(nameof(source.Type), source.Type, "Wrong TopItemModel Type");
        }
    }
}