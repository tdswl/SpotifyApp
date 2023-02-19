using AutoMapper;
using AutoMapper.Extensions.EnumMapping;
using SpotifyApp.Api.Client.OpenApiClient;
using SpotifyApp.Shared.AutoMapper.Resolvers;
using SpotifyApp.Shared.Enums;
using SpotifyApp.Shared.Models;
using ArtistModel = SpotifyApp.Shared.Models.ArtistModel;

namespace SpotifyApp.Shared.AutoMapper;

public sealed class ApplicationMapProfile : Profile
{
    public ApplicationMapProfile()
    {
        CreateMap<AlbumBaseAlbum_type, AlbumType>()
            .ConvertUsingEnumMapping(opt => opt
                .MapValue(AlbumBaseAlbum_type.Compilation, AlbumType.Compilation)
                .MapValue(AlbumBaseAlbum_type.Single, AlbumType.Single)
                .MapValue(AlbumBaseAlbum_type.Album, AlbumType.Album)
            )
            .ReverseMap();
        
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
        
        CreateMap<PrivateUserObject, UserModel>()
            .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Display_name))
            .ForMember(d => d.Product, opt => opt.MapFrom<ProductToEnumResolver>())
            .ValidateMemberList(MemberList.Destination);
        
        CreateMap<ArtistObject, ArtistModel>()
            .ValidateMemberList(MemberList.Destination);

        CreateMap<SimplifiedTrackObject, TrackModel>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
            .ForMember(d => d.Explicit, opt => opt.MapFrom(s => s.Explicit))
            .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
            .ForMember(d => d.Images, opt => opt.Ignore())
            .ForMember(d => d.ArtistName,
                opt => opt.MapFrom(s => string.Join(", ", s.Artists.Select(a => a.Name))))
            .ForMember(d => d.AlbumName, opt => opt.Ignore())
            .ForMember(d => d.DurationMs,
                opt => opt.MapFrom(s => TimeSpan.FromMilliseconds(s.Duration_ms).ToString(@"mm\:ss")))
            .ForMember(d => d.Index, opt => opt.Ignore())
            .ValidateMemberList(MemberList.Destination);
        
        CreateMap<SimplifiedAlbumObject, AlbumModel>()
            .ForMember(d => d.AlbumType, opt => opt.MapFrom(s => s.Album_type))
            .ValidateMemberList(MemberList.Destination);
    }
}