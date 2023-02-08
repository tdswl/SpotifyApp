using AutoMapper;
using SpotifyApp.Api.Contracts.Users.Models;
using SpotifyApp.Api.Contracts.Users.Responses;
using SpotifyApp.Shared.AutoMapper.Resolvers;
using SpotifyApp.Shared.Models;
using Image = SpotifyApp.Api.Contracts.Users.Models.Image;

namespace SpotifyApp.Shared.AutoMapper;

public sealed class ApplicationMapProfile : Profile
{
    public ApplicationMapProfile()
    {
        CreateMap<Image, Models.Image>()
            .ValidateMemberList(MemberList.Destination);

        CreateMap<GetCurrentUserProfileResponse, UserModel>()
            .ValidateMemberList(MemberList.Destination);
        
        CreateMap<TopItemModel, ItemModel>()
            .ForMember(d => d.ItemType, opt => opt.MapFrom(new ApiItemTypeToAppTypeResolver()))
            .ValidateMemberList(MemberList.Destination);
    }
}