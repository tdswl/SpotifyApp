namespace SpotifyApp.Api.Client.Tracks;

public static class TracksRoutes
{
    public const string GetTrack = "https://api.spotify.com/v1/tracks";

    public const string GetUsersSavedTracks = "https://api.spotify.com/v1/me/tracks";

    public const string CheckUsersSavedTracks = "https://api.spotify.com/v1/me/tracks/contains";

    public const string GetTracksAudioFeatures = "https://api.spotify.com/v1/audio-features";

    public const string GetTracksAudioAnalysis = "https://api.spotify.com/v1/audio-analysis";

    public const string GetRecommendations = "https://api.spotify.com/v1/recommendations";
}