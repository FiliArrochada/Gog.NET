using System.Text.Json;
using GogNet.Client;
using GogNet.Model;
using Xunit;

namespace GogNet.Test.Model
{
    /// <summary>Models round-trip through the shared serializer options unchanged.</summary>
    public class RoundTripTests
    {
        [Fact]
        public void Game_round_trips()
        {
            var game = new Game
            {
                Id = "1425308948",
                Slug = "afterlife",
                ProductType = "GAME",
                ReleaseStatus = "unavailable",
                IsUsingDosBox = true,
                Links = new GameLinks
                {
                    BoxArtImage = new Link { Href = "https://images.gog-statics.com/box.jpg" },
                    Icon = new Link { Href = "https://images.gog-statics.com/icon.png" }
                }
            };

            var json = JsonSerializer.Serialize(game, SerializerOptions.Default);
            var back = JsonSerializer.Deserialize<Game>(json, SerializerOptions.Default);

            Assert.Equal(game, back);
            Assert.Equal("https://images.gog-statics.com/box.jpg", back.BoxArtUrl);
        }

        [Fact]
        public void Product_round_trips()
        {
            var product = new Product
            {
                Id = 1425308948,
                Title = "Afterlife",
                Slug = "afterlife",
                Images = new ProductImages { Logo2x = "//images.gog-statics.com/logo_2x.jpg" }
            };

            var json = JsonSerializer.Serialize(product, SerializerOptions.Default);
            var back = JsonSerializer.Deserialize<Product>(json, SerializerOptions.Default);

            Assert.Equal(product, back);
            Assert.Equal("https://images.gog-statics.com/logo_2x.jpg", back.TileLogoUrl);
        }

        [Theory]
        [InlineData("https://images.gog-statics.com/x_{formatter}.{ext}", "glx_logo_2x", "https://images.gog-statics.com/x_glx_logo_2x.jpg")]
        [InlineData("//images.gog-statics.com/x.jpg", "", "https://images.gog-statics.com/x.jpg")]
        public void Link_resolves_templates_and_protocol_relative_urls(string href, string formatter, string expected)
        {
            var link = new Link { Href = href };
            Assert.Equal(expected, link.ResolveHref(formatter));
        }
    }
}
