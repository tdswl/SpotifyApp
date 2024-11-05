using AutoMapper;
using SpotifyApp.Shared.AutoMapper;

namespace SpotifyApp.Shared.Tests.AutoMapper;

public sealed class AutoMapperTests
{
    private readonly IMapper _mapper;

    public AutoMapperTests()
    {
        var config = new MapperConfiguration(opts =>
        {
            opts.AddProfile<ApplicationMapProfile>();
        });

        _mapper = config.CreateMapper();
    }

    [Fact]
    public void Mapper_Configuration_Should_Be_Valid()
    {
        _mapper.ConfigurationProvider.AssertConfigurationIsValid();
    }
}