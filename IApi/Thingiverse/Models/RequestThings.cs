using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLinker.Thingiverse.Models
{
    public partial class RequestThings
    {
        [JsonProperty("total")]
        public long Total { get; set; }

        [JsonProperty("hits")]
        public List<Hit> Hits { get; set; }
    }

    public partial class Hit
    {
        [JsonProperty("id")]
        public long Id { get; private set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("public_url")]
        public Uri PublicUrl { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("thumbnail")]
        public Uri Thumbnail { get; set; }

        [JsonProperty("preview_image")]
        public Uri PreviewImage { get; set; }

        [JsonProperty("creator")]
        public Creator Creator { get; set; }

        [JsonProperty("comment_count", NullValueHandling = NullValueHandling.Ignore)]
        public long CommentCount { get; set; }

        [JsonProperty("make_count")]
        public long MakeCount { get; set; }

        [JsonProperty("like_count")]
        public long LikeCount { get; set; }

        [JsonProperty("tags")]
        public List<Tag> Tags { get; set; }

        [JsonProperty("is_nsfw")]
        public bool IsNsfw { get; set; }
    }
}
