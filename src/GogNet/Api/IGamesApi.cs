/*
 * GOG.com API — v2 games
 *
 * api.gog.com/v2/games/{id}: the HAL-style endpoint that carries real artwork links
 * (boxArtImage, backgroundImage, galaxyBackgroundImage, icon). Anonymous: no token,
 * no account. See api/gog-api-v2.0.0.yaml.
 */

using System.Collections.Generic;
using System.Threading.Tasks;
using GogNet.Model;
using Refit;

namespace GogNet.Api
{
    public interface IGamesApi
    {
        /// <summary>One game by product id, with artwork and store links.</summary>
        [Get("/v2/games/{id}")]
        Task<Game> GetGameAsync(string id, [AliasAs("locale")] string? locale = null);

        /// <summary>
        /// Several games in one call. GOG expects a comma-joined list in a single
        /// <c>ids</c> parameter, which is why this takes a pre-joined string.
        /// </summary>
        [Get("/v2/games")]
        Task<GameList> GetGamesAsync([AliasAs("ids")] string commaSeparatedIds,
                                     [AliasAs("locale")] string? locale = null);
    }
}
