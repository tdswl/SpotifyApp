using System.Net.Http.Headers;
using System.Text;

namespace SpotifyApp.Api.Client.OpenApiClient
{
    public partial class SpotifyClient
    {
        private readonly ITokenService _tokenService;

        public SpotifyClient(ITokenService tokenService,
            IHttpClientFactory httpClientFactory) : this(httpClientFactory.CreateClient())
        {
            _tokenService = tokenService;
        }

        private async Task PrepareRequestAsync(HttpClient client,
            HttpRequestMessage request,
            StringBuilder url,
            CancellationToken cancellationToken)
        {
            var authToken = await _tokenService.GetAccessToken(cancellationToken);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
        }

        private async Task PrepareRequestAsync(HttpClient client,
            HttpRequestMessage request,
            string url,
            CancellationToken cancellationToken)
        {
            var authToken = await _tokenService.GetAccessToken(cancellationToken);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
        }

        private static Task ProcessResponseAsync(HttpClient client,
            HttpResponseMessage request,
            CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        #region FixedMethods

        // Fixed type - "A comma-separated list of item"
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>
        /// Search for Item
        /// </summary>
        /// <remarks>
        /// Get Spotify catalog information about albums, artists, playlists, tracks, shows, episodes or audiobooks
        /// <br/>that match a keyword string.&lt;br /&gt;
        /// <br/>**Note: Audiobooks are only available for the US, UK, Ireland, New Zealand and Australia markets.**
        /// </remarks>
        /// <returns>Search response</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public virtual async System.Threading.Tasks.Task<Response8> SearchAsync(string q, 
            System.Collections.Generic.IEnumerable<Anonymous> type, 
            string market, 
            int? limit, 
            int? offset, 
            Include_external? include_external, 
            System.Threading.CancellationToken cancellationToken)
        {
            if (q == null)
                throw new System.ArgumentNullException("q");

            if (type == null)
                throw new System.ArgumentNullException("type");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/search?");
            urlBuilder_.Append(System.Uri.EscapeDataString("q") + "=").Append(System.Uri.EscapeDataString(ConvertToString(q, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append(System.Uri.EscapeDataString("type") + "=")
                .Append(string.Join(",", type.Select(item_ => System.Uri.EscapeDataString(ConvertToString(item_, System.Globalization.CultureInfo.InvariantCulture)))))
                .Append("&");
            if (market != null)
            {
                urlBuilder_.Append(System.Uri.EscapeDataString("market") + "=").Append(System.Uri.EscapeDataString(ConvertToString(market, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            }
            if (limit != null)
            {
                urlBuilder_.Append(System.Uri.EscapeDataString("limit") + "=").Append(System.Uri.EscapeDataString(ConvertToString(limit, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            }
            if (offset != null)
            {
                urlBuilder_.Append(System.Uri.EscapeDataString("offset") + "=").Append(System.Uri.EscapeDataString(ConvertToString(offset, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            }
            if (include_external != null)
            {
                urlBuilder_.Append(System.Uri.EscapeDataString("include_external") + "=").Append(System.Uri.EscapeDataString(ConvertToString(include_external, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            }
            urlBuilder_.Length--;

            var client_ = _httpClient;
            var disposeClient_ = false;
            try
            {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                    request_.Method = new System.Net.Http.HttpMethod("GET");
                    request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    await PrepareRequestAsync(client_, request_, urlBuilder_, cancellationToken).ConfigureAwait(false);

                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);

                    await PrepareRequestAsync(client_, request_, url_, cancellationToken).ConfigureAwait(false);

                    var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    var disposeResponse_ = true;
                    try
                    {
                        var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        await ProcessResponseAsync(client_, response_, cancellationToken).ConfigureAwait(false);

                        var status_ = (int)response_.StatusCode;
                        if (status_ == 200)
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<Response8>(response_, headers_, cancellationToken).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            return objectResponse_.Object;
                        }
                        else
                        if (status_ == 401)
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<Response18>(response_, headers_, cancellationToken).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            throw new ApiException<Response18>("Bad or expired token. This can happen if the user revoked a token or\nthe access token has expired. You should re-authenticate the user.\n", status_, objectResponse_.Text, headers_, objectResponse_.Object, null);
                        }
                        else
                        if (status_ == 403)
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<Response19>(response_, headers_, cancellationToken).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            throw new ApiException<Response19>("Bad OAuth request (wrong consumer key, bad nonce, expired\ntimestamp...). Unfortunately, re-authenticating the user won\'t help here.\n", status_, objectResponse_.Text, headers_, objectResponse_.Object, null);
                        }
                        else
                        if (status_ == 429)
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<Response20>(response_, headers_, cancellationToken).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            throw new ApiException<Response20>("The app has exceeded its rate limits.\n", status_, objectResponse_.Text, headers_, objectResponse_.Object, null);
                        }
                        else
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + status_ + ").", status_, responseData_, headers_, null);
                        }
                    }
                    finally
                    {
                        if (disposeResponse_)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (disposeClient_)
                    client_.Dispose();
            }
        }

        #endregion
    }
}
