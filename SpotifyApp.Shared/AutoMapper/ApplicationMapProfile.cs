using AutoMapper;
using SpotifyApp.Api.Contracts.Users.Models;
using SpotifyApp.Api.Contracts.Users.Responses;
using SpotifyApp.Shared.AutoMapper.Resolvers;
using SpotifyApp.Shared.Enums;
using SpotifyApp.Shared.Models;
using Image = SpotifyApp.Api.Contracts.Users.Models.Image;

namespace SpotifyApp.Shared.AutoMapper;

public sealed class ApplicationMapProfile : Profile
{
    public ApplicationMapProfile()
    {
        CreateMap<Image, Models.Image>()
            .ValidateMemberList(MemberList.Destination);

        CreateMap<GetCurrentUserProfileResponse, ItemWithImages>()
            .ForMember(d => d.Name, opt => opt.MapFrom(s => s.DisplayName))
            .ForMember(d => d.ItemType, opt => opt.MapFrom(s => ItemType.Profile))
            .ValidateMemberList(MemberList.Destination);
        
        CreateMap<TopItemModel, ItemWithImages>()
            .ForMember(d => d.ItemType, opt => opt.MapFrom(new ApiItemTypeToAppTypeResolver()))
            .ValidateMemberList(MemberList.Destination);
    }
}