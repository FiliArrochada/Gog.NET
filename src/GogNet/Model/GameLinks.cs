/*
 * GOG.com API — v2 games "_links" block.
 *
 * boxArtImage is the portrait cover GOG shows in the store; the rest are the
 * background/icon variants Galaxy uses.
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
    public partial class GameLinks : IEquatable<GameLinks>, IValidatableObject
    {
        [DataMember(Name = "self", EmitDefaultValue = false)]
        [JsonPropertyName("self")]
        public Link Self { get; set; }

        [DataMember(Name = "store", EmitDefaultValue = false)]
        [JsonPropertyName("store")]
        public Link Store { get; set; }

        [DataMember(Name = "support", EmitDefaultValue = false)]
        [JsonPropertyName("support")]
        public Link Support { get; set; }

        [DataMember(Name = "forum", EmitDefaultValue = false)]
        [JsonPropertyName("forum")]
        public Link Forum { get; set; }

        [DataMember(Name = "icon", EmitDefaultValue = false)]
        [JsonPropertyName("icon")]
        public Link Icon { get; set; }

        /// <summary>Portrait box art — the real cover image.</summary>
        [DataMember(Name = "boxArtImage", EmitDefaultValue = false)]
        [JsonPropertyName("boxArtImage")]
        public Link BoxArtImage { get; set; }

        [DataMember(Name = "backgroundImage", EmitDefaultValue = false)]
        [JsonPropertyName("backgroundImage")]
        public Link BackgroundImage { get; set; }

        [DataMember(Name = "galaxyBackgroundImage", EmitDefaultValue = false)]
        [JsonPropertyName("galaxyBackgroundImage")]
        public Link GalaxyBackgroundImage { get; set; }

        public string ToJson() => JsonSerializer.Serialize(this, SerializerOptions.Indented);

        public override string ToString() =>
            "class GameLinks {\n  BoxArtImage: " + BoxArtImage +
            "\n  BackgroundImage: " + BackgroundImage +
            "\n  Icon: " + Icon + "\n}\n";

        public override bool Equals(object input) => Equals(input as GameLinks);

        public bool Equals(GameLinks input) =>
            input != null &&
            Equals(Self, input.Self) &&
            Equals(Store, input.Store) &&
            Equals(Support, input.Support) &&
            Equals(Forum, input.Forum) &&
            Equals(Icon, input.Icon) &&
            Equals(BoxArtImage, input.BoxArtImage) &&
            Equals(BackgroundImage, input.BackgroundImage) &&
            Equals(GalaxyBackgroundImage, input.GalaxyBackgroundImage);

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 41;
                if (Self != null) hash = hash * 59 + Self.GetHashCode();
                if (Store != null) hash = hash * 59 + Store.GetHashCode();
                if (Icon != null) hash = hash * 59 + Icon.GetHashCode();
                if (BoxArtImage != null) hash = hash * 59 + BoxArtImage.GetHashCode();
                if (BackgroundImage != null) hash = hash * 59 + BackgroundImage.GetHashCode();
                return hash;
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) =>
            new List<ValidationResult>();
    }
}
