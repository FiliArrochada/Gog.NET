using System.IO;
using System.Threading.Tasks;
using GogNet.Api;
using GogNet.Test.RefitSlice;
using Xunit;

namespace GogNet.Test.Refit
{
    /// <summary>
    /// Wire-shape tests for the v1 products endpoint. The image block only comes back
    /// when expand=images is sent, so that query has to survive refactors.
    /// </summary>
    public class ProductsApiTests
    {
        private static string Fixture(string name) =>
            File.ReadAllText(Path.Combine("Fixtures", name));

        [Fact]
        public async Task GetProduct_sends_expand_and_promotes_protocol_relative_urls()
        {
            var handler = new CapturingHandler(Fixture("products-get-v1.json"));
            var api = GogRefit.Create<IProductsApi>(GogRefit.Client(handler));

            var product = await api.GetProductAsync("1425308948", "images");

            Assert.Equal("/products/1425308948", handler.LastRequest.RequestUri.AbsolutePath);
            Assert.Contains("expand=images", handler.LastRequest.RequestUri.Query);
            Assert.Equal("Afterlife", product.Title);
            Assert.StartsWith("https://", product.TileLogoUrl);
            Assert.Contains("glx_logo_2x", product.TileLogoUrl);
        }

        [Fact]
        public async Task GetProduct_omits_expand_when_not_asked()
        {
            var handler = new CapturingHandler(Fixture("products-get-v1.json"));
            var api = GogRefit.Create<IProductsApi>(GogRefit.Client(handler));

            await api.GetProductAsync("1425308948");

            Assert.DoesNotContain("expand", handler.LastRequest.RequestUri.Query);
        }
    }
}
