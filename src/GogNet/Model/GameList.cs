/*
 * GOG.com API — response of GET /v2/games?ids=...
 *
 * GOG returns the games under an "items" array (HAL "_embedded" in some responses);
 * both shapes are accepted so callers get a list either way.
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
    public partial class GameList : IEquatable<GameList>, IValidatableObject
    {
        [DataMember(Name = "items", EmitDefaultValue = false)]
        [JsonPropertyName("items")]
        public List<Game> Items { get; set; }

        [DataMember(Name = "_embedded", EmitDefaultValue = false)]
        [JsonPropertyName("_embedded")]
        public GameListEmbedded Embedded { get; set; }

        /// <summary>The games from whichever shape the response used.</summary>
        [JsonIgnore]
        public IReadOnlyList<Game> Games =>
            Items ?? Embedded?.Items ?? new List<Game>();

        public string ToJson() => JsonSerializer.Serialize(this, SerializerOptions.Indented);

        public override string ToString() => "class GameList {\n  Count: " + Games.Count + "\n}\n";

        public override bool Equals(object input) => Equals(input as GameList);

        public bool Equals(GameList input) =>
            input != null && Games.Count == input.Games.Count;

        public override int GetHashCode() => Games.Count;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) =>
            new List<ValidationResult>();
    }

    [DataContract]
    public partial class GameListEmbedded
    {
        [DataMember(Name = "items", EmitDefaultValue = false)]
        [JsonPropertyName("items")]
        public List<Game> Items { get; set; }
    }
}
