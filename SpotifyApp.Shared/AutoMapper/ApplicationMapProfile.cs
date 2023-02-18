using AutoMapper;
using AutoMapper.Extensions.EnumMapping;
using SpotifyApp.Api.Client.OpenApiClient;
using SpotifyApp.Api.Contracts.Albums.Models;
using SpotifyApp.Api.Contracts.Base.Enums;
using SpotifyApp.Api.Contracts.Base.Models;
using SpotifyApp.Api.Contracts.Tracks.Enums;
using SpotifyApp.Api.Contracts.Tracks.Models;
using SpotifyApp.Api.Contracts.Users.Responses;
using SpotifyApp.Shared.Enums;
using SpotifyApp.Shared.Models;
using ArtistModel = SpotifyApp.Shared.Models.ArtistModel;

namespace SpotifyApp.Shared.AutoMapper;

public sealed class ApplicationMapProfile : Profile
{
    public ApplicationMapProfile()
    {
        CreateMap<ImageObject, ImageModel>()
            .ValidateMemberList(MemberList.Destination);
        
        CreateMap<TrackObject, TrackModel>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
            .ForMember(d => d.Explicit, opt => opt.MapFrom(s => s.Explicit))
            .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
            .ForMember(d => d.Images, opt => opt.MapFrom(s => s.Album.Images))
            .ForMember(d => d.ArtistName,
                opt => opt.MapFrom(s => string.Join(", ", s.Album.Artists.Select(a => a.Name))))
            .ForMember(d => d.AlbumName, opt => opt.MapFrom(s => string.Join(", ", s.Album.Name)))
            .ForMember(d => d.DurationMs, 
                opt => opt.MapFrom(s => TimeSpan.FromMilliseconds(s.Duration_ms).ToString(@"mm\:ss")))
            .ForMember(d => d.Index, opt => opt.Ignore())
            .ValidateMemberList(MemberList.Destination);

        CreateMap<ItemsTypeApi, ItemType>()
            .ConvertUsingEnumMapping(opt => opt
                .MapValue(ItemsTypeApi.Artist, ItemType.Artist)
                .MapValue(ItemsTypeApi.Track, ItemType.Track)
                .MapValue(ItemsTypeApi.User, ItemType.User)
                .MapValue(ItemsTypeApi.Album, ItemType.Album)
                .MapValue(ItemsTypeApi.AudioFeatures, ItemType.AudioFeatures)
                .MapValue(ItemsTypeApi.Genre, ItemType.Genre)
                .MapValue(ItemsTypeApi.Playlist, ItemType.Playlist)
            )
            .ReverseMap();
        
        CreateMap<AlbumGroupApi, AlbumGroup>()
            .ConvertUsingEnumMapping(opt => opt
                .MapValue(AlbumGroupApi.AppearsOn, AlbumGroup.AppearsOn)
                .MapValue(AlbumGroupApi.Compilation, AlbumGroup.Compilation)
                .MapValue(AlbumGroupApi.Single, AlbumGroup.Single)
                .MapValue(AlbumGroupApi.Album, AlbumGroup.Album)
            )
            .ReverseMap();
        
        CreateMap<ImageModelApi, ImageModel>()
            .ValidateMemberList(MemberList.Destination);

        CreateMap<GetCurrentUserProfileResponse, UserModel>()
            .ForMember(d => d.Name, opt => opt.MapFrom(s => s.DisplayName))
            .ValidateMemberList(MemberList.Destination);
        
        CreateMap<ItemModelApi, ArtistModel>()
            .ValidateMemberList(MemberList.Destination);

        CreateMap<TrackApiModel, TrackModel>()
            .ForMember(d => d.Images, opt => opt.MapFrom(s => s.Album.Images))
            .ForMember(d => d.ArtistName,
                opt => opt.MapFrom(s => string.Join(", ", s.Album.Artists.Select(a => a.Name))))
            .ForMember(d => d.AlbumName, opt => opt.MapFrom(s => string.Join(", ", s.Album.Name)))
            .ForMember(d => d.DurationMs, 
                opt => opt.MapFrom(s => TimeSpan.FromMilliseconds(s.DurationMs).ToString(@"mm\:ss")))
            .ForMember(d => d.Index, opt => opt.Ignore())
            .ValidateMemberList(MemberList.Destination);
        
        CreateMap<AlbumApiModel, AlbumModel>()
            .ValidateMemberList(MemberList.Destination);
    }
}