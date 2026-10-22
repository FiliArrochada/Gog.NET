/*
 * GOG.com API
 *
 * RefitSettings factory: GOG has no URL quirks, so this is just STJ wiring plus the
 * two base addresses, kept in one place so consumers do not hardcode them.
 */

using System;
using System.Net.Http;
using Refit;

namespace GogNet.Client
{
    /// <summary>
    /// Builds the <see cref="RefitSettings"/> and <see cref="HttpClient"/> instances for the
    /// public GOG APIs. Everything here is anonymous — the public endpoints need no token.
    /// Callers own the <see cref="HttpClient"/> lifetime.
    /// </summary>
    public static class GogRefitSettings
    {
        /// <summary>Base address for both api.gog.com API versions.</summary>
        public static readonly Uri ApiBaseAddress = new Uri("https://api.gog.com/");

        public static RefitSettings Create() =>
            new RefitSettings(new SystemTextJsonContentSerializer(SerializerOptions.Default));

        /// <summary>Convenience client pointed at api.gog.com; add handlers as needed.</summary>
        public static HttpClient CreateHttpClient() =>
            new HttpClient { BaseAddress = ApiBaseAddress };
    }
}
