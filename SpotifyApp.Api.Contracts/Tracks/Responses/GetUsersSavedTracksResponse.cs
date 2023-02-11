using SpotifyApp.Api.Contracts.Base.Responses;
using SpotifyApp.Api.Contracts.Tracks.Models;

namespace SpotifyApp.Api.Contracts.Tracks.Responses;

public sealed class GetUsersSavedTracksResponse : PagedResponse<SavedTracksModel>
{
}