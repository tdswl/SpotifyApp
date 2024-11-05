using AutoMapper;
using SpotifyApp.Api.Client.OpenApiClient;
using SpotifyApp.Shared.Enums;

namespace SpotifyApp.Shared.AutoMapper.Resolvers;

public sealed class ProductToEnumResolver : IValueResolver<PrivateUserObject, object, ProductType>
{
    public ProductType Resolve(PrivateUserObject source, object dest, ProductType destMember, ResolutionContext context)
    {
        return source.Product.ToUpperInvariant() switch
        {
            "OPEN" => ProductType.Free,
            "FREE" => ProductType.Free,
            "PREMIUM" => ProductType.Premium,
            _ => ProductType.Free
        };
    }
}