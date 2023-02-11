using AutoMapper;
using AutoMapper.Extensions.EnumMapping;
using SpotifyApp.Api.Contracts.Users.Enums;
using SpotifyApp.Api.Contracts.Users.Models;
using SpotifyApp.Api.Contracts.Users.Responses;
using SpotifyApp.Shared.Enums;
using SpotifyApp.Shared.Models;
using Image = SpotifyApp.Api.Contracts.Users.Models.Image;
using ItemModel = SpotifyApp.Api.Contracts.Base.Models.ItemModel;

namespace SpotifyApp.Shared.AutoMapper;

public sealed class ApplicationMapProfile : Profile
{
    public ApplicationMapProfile()
    {
        CreateMap<ItemsTypeApi, ItemType>()
            .ConvertUsingEnumMapping(opt => opt
                .MapValue(ItemsTypeApi.Artist, ItemType.Artist)
                .MapValue(ItemsTypeApi.Track, ItemType.Track)
                .MapValue(ItemsTypeApi.User, ItemType.User)
            )
            .ReverseMap();
        
        CreateMap<Image, Models.Image>()
            .ValidateMemberList(MemberList.Destination);

        CreateMap<GetCurrentUserProfileResponse, UserModel>()
            .ValidateMemberList(MemberList.Destination);
        
        CreateMap<ItemModel, Models.ItemModel>()
            .ValidateMemberList(MemberList.Destination);
    }
}