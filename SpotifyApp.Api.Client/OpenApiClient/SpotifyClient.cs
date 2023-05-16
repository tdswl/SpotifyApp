using System.Diagnostics.CodeAnalysis;
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
        
#pragma warning disable 108 // Disable "CS0108 '{derivedDto}.ToJson()' hides inherited member '{dtoBase}.ToJson()'. Use the new keyword if hiding was intended."
#pragma warning disable 114 // Disable "CS0114 '{derivedDto}.RaisePropertyChanged(String)' hides inherited member 'dtoBase.RaisePropertyChanged(String)'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword."
#pragma warning disable 472 // Disable "CS0472 The result of the expression is always 'false' since a value of type 'Int32' is never equal to 'null' of type 'Int32?'
#pragma warning disable 1573 // Disable "CS1573 Parameter '...' has no matching param tag in the XML comment for ...
#pragma warning disable 1591 // Disable "CS1591 Missing XML comment for publicly visible type or member ..."
#pragma warning disable 8073 // Disable "CS8073 The result of the expression is always 'false' since a value of type 'T' is never equal to 'null' of type 'T?'"
#pragma warning disable 3016 // Disable "CS3016 Arrays as attribute arguments is not CLS-compliant"
#pragma warning disable 8603 // Disable "CS8603 Possible null reference return"

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
        [SuppressMessage("ReSharper", "RedundantNameQualifier")]
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        [SuppressMessage("ReSharper", "ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract")]
        [SuppressMessage("ReSharper", "UseNameofExpression")]
        [SuppressMessage("ReSharper", "InvokeAsExtensionMethod")]
        [SuppressMessage("ReSharper", "MethodSupportsCancellation")]
        [SuppressMessage("ReSharper", "MergeIntoPattern")]
        [SuppressMessage("ReSharper", "ConvertToUsingDeclaration")]
        public async System.Threading.Tasks.Task<Response8> SearchAsync(string q, 
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

#pragma warning restore 1591
#pragma warning restore 1573
#pragma warning restore  472
#pragma warning restore  114
#pragma warning restore  108
#pragma warning restore 3016
#pragma warning restore 8603

        #endregion
    }
}
