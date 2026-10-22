/*
 * GOG.com API — v1 product "images" block (expand=images).
 *
 * No box art here: these are the Galaxy tile logos, backgrounds and icons.
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
    public partial class ProductImages : IEquatable<ProductImages>, IValidatableObject
    {
        [DataMember(Name = "logo", EmitDefaultValue = false)]
        [JsonPropertyName("logo")]
        public string Logo { get; set; }

        [DataMember(Name = "logo2x", EmitDefaultValue = false)]
        [JsonPropertyName("logo2x")]
        public string Logo2x { get; set; }

        [DataMember(Name = "background", EmitDefaultValue = false)]
        [JsonPropertyName("background")]
        public string Background { get; set; }

        [DataMember(Name = "icon", EmitDefaultValue = false)]
        [JsonPropertyName("icon")]
        public string Icon { get; set; }

        [DataMember(Name = "sidebarIcon", EmitDefaultValue = false)]
        [JsonPropertyName("sidebarIcon")]
        public string SidebarIcon { get; set; }

        public string ToJson() => JsonSerializer.Serialize(this, SerializerOptions.Indented);

        public override string ToString() =>
            "class ProductImages {\n  Logo2x: " + Logo2x + "\n  Background: " + Background + "\n}\n";

        public override bool Equals(object input) => Equals(input as ProductImages);

        public bool Equals(ProductImages input) =>
            input != null &&
            Logo == input.Logo &&
            Logo2x == input.Logo2x &&
            Background == input.Background &&
            Icon == input.Icon &&
            SidebarIcon == input.SidebarIcon;

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 41;
                if (Logo2x != null) hash = hash * 59 + Logo2x.GetHashCode();
                if (Background != null) hash = hash * 59 + Background.GetHashCode();
                if (Icon != null) hash = hash * 59 + Icon.GetHashCode();
                return hash;
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) =>
            new List<ValidationResult>();
    }
}
