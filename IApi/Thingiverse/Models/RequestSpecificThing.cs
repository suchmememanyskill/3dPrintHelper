using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLinker.Thingiverse.Models
{
    public class RequestSpecificThing
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("thumbnail")]
        public Uri Thumbnail { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("public_url")]
        public Uri PublicUrl { get; set; }

        [JsonProperty("creator")]
        public Creator Creator { get; set; }

        [JsonProperty("added")]
        public DateTimeOffset Added { get; set; }

        [JsonProperty("modified")]
        public DateTimeOffset Modified { get; set; }

        [JsonProperty("is_published")]
        public long IsPublished { get; set; }

        [JsonProperty("is_wip")]
        public long IsWip { get; set; }

        [JsonProperty("is_featured")]
        public object IsFeatured { get; set; }

        [JsonProperty("is_nsfw")]
        public bool IsNsfw { get; set; }

        [JsonProperty("like_count")]
        public long LikeCount { get; set; }

        [JsonProperty("is_liked")]
        public bool IsLiked { get; set; }

        [JsonProperty("collect_count")]
        public long CollectCount { get; set; }

        [JsonProperty("is_collected")]
        public bool IsCollected { get; set; }

        [JsonProperty("comment_count")]
        public long CommentCount { get; set; }

        [JsonProperty("is_watched")]
        public bool IsWatched { get; set; }

        [JsonProperty("default_image")]
        public DefaultImage DefaultImage { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("instructions")]
        public string Instructions { get; set; }

        [JsonProperty("description_html")]
        public string DescriptionHtml { get; set; }

        [JsonProperty("instructions_html")]
        public string InstructionsHtml { get; set; }

        [JsonProperty("details")]
        public string Details { get; set; }

        [JsonProperty("edu_details")]
        public string EduDetails { get; set; }

        [JsonProperty("license")]
        public string License { get; set; }

        [JsonProperty("allows_derivatives")]
        public bool AllowsDerivatives { get; set; }

        [JsonProperty("files_url")]
        public Uri FilesUrl { get; set; }

        [JsonProperty("images_url")]
        public Uri ImagesUrl { get; set; }

        [JsonProperty("likes_url")]
        public Uri LikesUrl { get; set; }

        [JsonProperty("ancestors_url")]
        public Uri AncestorsUrl { get; set; }

        [JsonProperty("derivatives_url")]
        public Uri DerivativesUrl { get; set; }

        [JsonProperty("tags_url")]
        public Uri TagsUrl { get; set; }

        [JsonProperty("tags")]
        public List<Tag> Tags { get; set; }

        [JsonProperty("categories_url")]
        public Uri CategoriesUrl { get; set; }

        [JsonProperty("file_count")]
        public long FileCount { get; set; }

        [JsonProperty("layout_count")]
        public long LayoutCount { get; set; }

        [JsonProperty("layouts_url")]
        public Uri LayoutsUrl { get; set; }

        [JsonProperty("is_private")]
        public long IsPrivate { get; set; }

        [JsonProperty("is_purchased")]
        public long IsPurchased { get; set; }

        [JsonProperty("in_library")]
        public bool InLibrary { get; set; }

        [JsonProperty("print_history_count")]
        public long PrintHistoryCount { get; set; }

        [JsonProperty("app_id")]
        public object AppId { get; set; }

        [JsonProperty("download_count")]
        public long DownloadCount { get; set; }

        [JsonProperty("view_count")]
        public long ViewCount { get; set; }

        [JsonProperty("remix_count")]
        public long RemixCount { get; set; }

        [JsonProperty("make_count")]
        public long MakeCount { get; set; }

        [JsonProperty("app_count")]
        public long AppCount { get; set; }

        [JsonProperty("root_comment_count")]
        public long RootCommentCount { get; set; }

        [JsonProperty("moderation")]
        public string Moderation { get; set; }

        [JsonProperty("is_derivative")]
        public bool IsDerivative { get; set; }

        [JsonProperty("ancestors")]
        public List<object> Ancestors { get; set; }

        [JsonProperty("can_comment")]
        public bool CanComment { get; set; }

        [JsonProperty("type_name")]
        public string TypeName { get; set; }
    }

    public partial class Creator
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("public_url")]
        public Uri PublicUrl { get; set; }

        [JsonProperty("thumbnail")]
        public Uri Thumbnail { get; set; }

        [JsonProperty("count_of_followers")]
        public long CountOfFollowers { get; set; }

        [JsonProperty("count_of_following")]
        public long CountOfFollowing { get; set; }

        [JsonProperty("count_of_designs")]
        public long CountOfDesigns { get; set; }

        [JsonProperty("accepts_tips")]
        public bool AcceptsTips { get; set; }

        [JsonProperty("is_following")]
        public bool IsFollowing { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("cover")]
        public Uri Cover { get; set; }
    }

    public partial class DefaultImage
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("sizes")]
        public List<Size> Sizes { get; set; }

        [JsonProperty("added")]
        public DateTimeOffset Added { get; set; }
    }

    public partial class Size
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("size")]
        public string SizeSize { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }
    }

    public partial class Tag
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("tag")]
        public string TagTag { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("count")]
        public long Count { get; set; }

        [JsonProperty("things_url")]
        public Uri ThingsUrl { get; set; }

        [JsonProperty("absolute_url")]
        public Uri AbsoluteUrl { get; set; }
    }
}
