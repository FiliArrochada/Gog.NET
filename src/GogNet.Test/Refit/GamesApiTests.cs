using System.IO;
using System.Threading.Tasks;
using GogNet.Api;
using GogNet.Test.RefitSlice;
using Xunit;

namespace GogNet.Test.Refit
{
    /// <summary>
    /// Wire-shape tests for the v2 games endpoints: path, query and response parsing.
    /// These lock the URL format down — GOG takes ids as one comma-joined parameter.
    /// </summary>
    public class GamesApiTests
    {
        private static string Fixture(string name) =>
            File.ReadAllText(Path.Combine("Fixtures", name));

        [Fact]
        public async Task GetGame_hits_v2_path_and_parses_box_art()
        {
            var handler = new CapturingHandler(Fixture("games-get-v2.json"));
            var api = GogRefit.Create<IGamesApi>(GogRefit.Client(handler));

            var game = await api.GetGameAsync("1425308948");

            Assert.Equal("/v2/games/1425308948", handler.LastRequest.RequestUri.AbsolutePath);
            Assert.Equal("GET", handler.LastRequest.Method.Method);
            Assert.Equal("afterlife", game.Slug);
            Assert.StartsWith("https://images.gog-statics.com/", game.BoxArtUrl);
            Assert.EndsWith(".jpg", game.BoxArtUrl);
        }

        [Fact]
        public async Task GetGame_passes_locale_when_given()
        {
            var handler = new CapturingHandler(Fixture("games-get-v2.json"));
            var api = GogRefit.Create<IGamesApi>(GogRefit.Client(handler));

            await api.GetGameAsync("1425308948", "en-US");

            Assert.Contains("locale=en-US", handler.LastRequest.RequestUri.Query);
        }

        [Fact]
        public async Task GetGames_sends_ids_as_one_comma_joined_parameter()
        {
            var handler = new CapturingHandler("{\"items\":[]}");
            var api = GogRefit.Create<IGamesApi>(GogRefit.Client(handler));

            var list = await api.GetGamesAsync("1425308948,1430398832");

            Assert.Equal("/v2/games", handler.LastRequest.RequestUri.AbsolutePath);
            Assert.Contains("ids=1425308948%2C1430398832", handler.LastRequest.RequestUri.Query);
            Assert.Empty(list.Games);
        }
    }
}
