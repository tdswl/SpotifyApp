using SpotifyApp.Api.Contracts.Albums.Models;
using SpotifyApp.Api.Contracts.Albums.Requests;
using SpotifyApp.Api.Contracts.Albums.Responses;
using SpotifyApp.Api.Contracts.Base.Requests;
using SpotifyApp.Api.Contracts.Base.Responses;
using SpotifyApp.Api.Contracts.Tracks.Models;

namespace SpotifyApp.Api.Client.Albums;

public interface IAlbumsClient
{
    /// <summary>
    /// Get Spotify catalog information for a single album
    /// https://developer.spotify.com/documentation/web-api/reference/#/operations/get-an-album
    /// </summary>
    public Task<GetAlbumResponse> GetAlbum(IdRequest request, CancellationToken token);
    
    /// <summary>
    /// Get Spotify catalog information for multiple albums identified by their Spotify IDs.
    /// https://developer.spotify.com/documentation/web-api/reference/#/operations/get-multiple-albums
    /// </summary>
    public Task<GetSeveralAlbumsResponse> GetSeveralAlbums(IdsRequest request, CancellationToken token);
    
    /// <summary>
    /// Get Spotify catalog information about an album’s tracks.
    /// Optional parameters can be used to limit the number of tracks returned.
    /// https://developer.spotify.com/documentation/web-api/reference/#/operations/get-an-albums-tracks
    /// </summary>
    public Task<PagedResponse<TrackApiModel>> GetAlbumTracks(GetAlbumTracksRequest request, CancellationToken token);
    
    /// <summary>
    /// Get a list of the albums saved in the current Spotify user's 'Your Music' library.
    /// https://developer.spotify.com/documentation/web-api/reference/#/operations/get-users-saved-albums
    /// </summary>
    public Task<PagedResponse<SavedAlbumsModel>> GetUsersSavedAlbums(GetUsersSavedAlbumsRequest request,
        CancellationToken token);

    /// <summary>
    /// Save one or more albums to the current user's 'Your Music' library.
    /// https://developer.spotify.com/documentation/web-api/reference/#/operations/save-albums-user
    /// </summary>
    public Task SaveAlbumsForCurrentUser(string ids, CancellationToken token);
    
    /// <summary>
    /// Remove one or more albums from the current user's 'Your Music' library.
    /// https://developer.spotify.com/documentation/web-api/reference/#/operations/remove-albums-user
    /// </summary>
    public Task RemoveUsersSavedAlbums(string ids, CancellationToken token);
    
    /// <summary>
    /// Check if one or more albums is already saved in the current Spotify user's 'Your Music' library.
    /// https://developer.spotify.com/documentation/web-api/reference/#/operations/check-users-saved-albums
    /// </summary>
    public Task<IReadOnlyList<bool>> CheckUsersSavedAlbums(string ids, CancellationToken token);
    
    /// <summary>
    /// Get a list of new album releases featured in Spotify (shown, for example, on a Spotify player’s “Browse” tab).
    /// https://developer.spotify.com/documentation/web-api/reference/#/operations/get-new-releases
    /// </summary>
    public Task<GetNewReleasesResponse> GetNewReleases(GetNewReleasesRequest request, CancellationToken token);
}