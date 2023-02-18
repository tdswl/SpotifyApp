# SpotifyApp

Experimental app to test current state of Avalonia with Spotify Api

To work with app - add Spotify ClientId in SpotifyApp.Shared.Configurations.OidcConfiguration


How to update nswag api client:
1. Download fresh openapi schema https://developer.spotify.com/_data/documentation/web-api/reference/open-api-schema.yml
2. Install nodejs: https://nodejs.org/en/
3. Install https://www.npmjs.com/package/nswag ```npm install nswag -g```
4. Run ```nswag run SpotifyApp.Api.Client/OpenApiClient/nswag.json```