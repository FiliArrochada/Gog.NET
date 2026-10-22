/*
 * GOG.com API
 *
 * A single HAL link: { "href": "..." }. Some image links are templated with
 * {formatter}/{ext} placeholders, so callers get the raw href plus a helper.
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
    public partial class Link : IEquatable<Link>, IValidatableObject
    {
        [DataMember(Name = "href", EmitDefaultValue = false)]
        [JsonPropertyName("href")]
        public string Href { get; set; }

        /// <summary>
        /// The href with GOG's <c>{formatter}</c>/<c>{ext}</c> placeholders filled in.
        /// Protocol-relative hrefs (<c>//images...</c>) are promoted to https.
        /// </summary>
        public string ResolveHref(string formatter = "", string ext = "jpg")
        {
            if (string.IsNullOrEmpty(Href))
                return Href;

            var url = Href.Replace("{formatter}", formatter ?? "").Replace("{ext}", ext ?? "jpg");
            if (url.StartsWith("//", StringComparison.Ordinal))
                url = "https:" + url;
            return url;
        }

        public string ToJson() => JsonSerializer.Serialize(this, SerializerOptions.Indented);

        public override string ToString() => "class Link {\n  Href: " + Href + "\n}\n";

        public override bool Equals(object input) => Equals(input as Link);

        public bool Equals(Link input) => input != null && Href == input.Href;

        public override int GetHashCode() => Href == null ? 0 : Href.GetHashCode();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) =>
            new List<ValidationResult>();
    }
}
