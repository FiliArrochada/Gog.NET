using System.IO;
using System.Text.Json;
using GogNet.Client;
using GogNet.Model;
using Xunit;

namespace GogNet.Test.Fixtures
{
    /// <summary>
    /// Captured real GOG responses deserialize into the models. These catch upstream
    /// shape changes that the hand-written slice tests would otherwise miss.
    /// </summary>
    public class FixtureTests
    {
        private static string Read(string name) =>
            File.ReadAllText(Path.Combine("Fixtures", name));

        [Fact]
        public void V2_game_fixture_deserializes()
        {
            var game = JsonSerializer.Deserialize<Game>(Read("games-get-v2.json"), SerializerOptions.Default);

            Assert.Equal("1425308948", game.Id);
            Assert.NotNull(game.Links.BoxArtImage);
            Assert.NotNull(game.Links.BackgroundImage);
            Assert.NotNull(game.Links.GalaxyBackgroundImage);
            Assert.StartsWith("https://images.gog-statics.com/", game.BoxArtUrl);
        }

        [Fact]
        public void V1_product_fixture_deserializes_and_has_no_box_art()
        {
            var product = JsonSerializer.Deserialize<Product>(Read("products-get-v1.json"), SerializerOptions.Default);

            Assert.Equal("Afterlife", product.Title);
            Assert.NotNull(product.Images.Logo2x);
            Assert.NotNull(product.TileLogoUrl);
        }
    }
}
