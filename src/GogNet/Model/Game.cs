/*
 * GOG.com API — v2 game.
 *
 * Only the stable, useful fields are modelled: the artwork/store links plus enough
 * identity to match an install on disk. GOG returns a great deal more (descriptions,
 * requirements, screenshots); add fields here as they are needed.
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
using GogNet.Client;

namespace GogNet.Model
{
    [DataContract]
    public partial class Game : IEquatable<Game>, IValidatableObject
    {
        [DataMember(Name = "id", EmitDefaultValue = false)]
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [DataMember(Name = "slug", EmitDefaultValue = false)]
        [JsonPropertyName("slug")]
        public string Slug { get; set; }

        [DataMember(Name = "productType", EmitDefaultValue = false)]
        [JsonPropertyName("productType")]
        public string ProductType { get; set; }

        [DataMember(Name = "releaseStatus", EmitDefaultValue = false)]
        [JsonPropertyName("releaseStatus")]
        public string ReleaseStatus { get; set; }

        [DataMember(Name = "isUsingDosBox", EmitDefaultValue = false)]
        [JsonPropertyName("isUsingDosBox")]
        public bool? IsUsingDosBox { get; set; }

        [DataMember(Name = "_links", EmitDefaultValue = false)]
        [JsonPropertyName("_links")]
        public GameLinks Links { get; set; }

        /// <summary>
        /// Portrait box art URL, or null when GOG has none for this product.
        /// This is the field a launcher wants for a cover.
        /// </summary>
        [JsonIgnore]
        public string BoxArtUrl => Links?.BoxArtImage?.ResolveHref();

        public string ToJson() => JsonSerializer.Serialize(this, SerializerOptions.Indented);

        public override string ToString() =>
            "class Game {\n  Id: " + Id + "\n  Slug: " + Slug +
            "\n  ProductType: " + ProductType +
            "\n  ReleaseStatus: " + ReleaseStatus + "\n}\n";

        public override bool Equals(object input) => Equals(input as Game);

        public bool Equals(Game input) =>
            input != null &&
            Id == input.Id &&
            Slug == input.Slug &&
            ProductType == input.ProductType &&
            ReleaseStatus == input.ReleaseStatus &&
            IsUsingDosBox == input.IsUsingDosBox &&
            Equals(Links, input.Links);

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 41;
                if (Id != null) hash = hash * 59 + Id.GetHashCode();
                if (Slug != null) hash = hash * 59 + Slug.GetHashCode();
                if (ProductType != null) hash = hash * 59 + ProductType.GetHashCode();
                if (Links != null) hash = hash * 59 + Links.GetHashCode();
                return hash;
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) =>
            new List<ValidationResult>();
    }
}
