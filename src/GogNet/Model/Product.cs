/*
 * GOG.com API — v1 product.
 *
 * Kept to the fields a launcher needs: identity, the store slug and the image block
 * (only present when the request asks for expand=images).
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
    public partial class Product : IEquatable<Product>, IValidatableObject
    {
        [DataMember(Name = "id", EmitDefaultValue = false)]
        [JsonPropertyName("id")]
        public long? Id { get; set; }

        [DataMember(Name = "title", EmitDefaultValue = false)]
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [DataMember(Name = "slug", EmitDefaultValue = false)]
        [JsonPropertyName("slug")]
        public string Slug { get; set; }

        [DataMember(Name = "images", EmitDefaultValue = false)]
        [JsonPropertyName("images")]
        public ProductImages Images { get; set; }

        /// <summary>
        /// Best available artwork from the v1 endpoint (the Galaxy tile logo), with
        /// protocol-relative URLs promoted to https. Null when no image block was requested.
        /// </summary>
        [JsonIgnore]
        public string TileLogoUrl
        {
            get
            {
                var url = Images?.Logo2x ?? Images?.Logo ?? Images?.Background;
                if (string.IsNullOrEmpty(url))
                    return null;
                return url.StartsWith("//", StringComparison.Ordinal) ? "https:" + url : url;
            }
        }

        public string ToJson() => JsonSerializer.Serialize(this, SerializerOptions.Indented);

        public override string ToString() =>
            "class Product {\n  Id: " + Id + "\n  Title: " + Title + "\n  Slug: " + Slug + "\n}\n";

        public override bool Equals(object input) => Equals(input as Product);

        public bool Equals(Product input) =>
            input != null &&
            Id == input.Id &&
            Title == input.Title &&
            Slug == input.Slug &&
            Equals(Images, input.Images);

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 41;
                if (Id != null) hash = hash * 59 + Id.GetHashCode();
                if (Title != null) hash = hash * 59 + Title.GetHashCode();
                if (Images != null) hash = hash * 59 + Images.GetHashCode();
                return hash;
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) =>
            new List<ValidationResult>();
    }
}
