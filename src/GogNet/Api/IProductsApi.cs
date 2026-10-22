/*
 * GOG.com API — v1 products
 *
 * api.gog.com/products/{id}: the older endpoint. It has no box art, but it does carry
 * the Galaxy tile logos, which is the only artwork available for some older products.
 */

using System.Threading.Tasks;
using GogNet.Model;
using Refit;

namespace GogNet.Api
{
    public interface IProductsApi
    {
        /// <summary>
        /// One product. Pass <c>expand=images</c> (or a comma-joined list) to include the
        /// image block — GOG omits it otherwise.
        /// </summary>
        [Get("/products/{id}")]
        Task<Product> GetProductAsync(string id, [AliasAs("expand")] string? expand = null);
    }
}
