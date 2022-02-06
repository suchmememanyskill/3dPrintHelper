using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLinker.MyMiniFactory.Models
{
    public class FetchSpecificObject
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("visibility")]
        public long Visibility { get; set; }

        [JsonProperty("visibility_name")]
        public string VisibilityName { get; set; }

        [JsonProperty("listed")]
        public bool Listed { get; set; }

        [JsonProperty("status")]
        public long Status { get; set; }

        [JsonProperty("status_name")]
        public string StatusName { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("description_html")]
        public string DescriptionHtml { get; set; }

        [JsonProperty("printing_details")]
        public string PrintingDetails { get; set; }

        [JsonProperty("printing_details_html")]
        public string PrintingDetailsHtml { get; set; }

        [JsonProperty("featured")]
        public bool Featured { get; set; }

        [JsonProperty("support")]
        public bool Support { get; set; }

        [JsonProperty("complexity")]
        public object Complexity { get; set; }

        [JsonProperty("complexity_name")]
        public object ComplexityName { get; set; }

        [JsonProperty("dimensions")]
        public string Dimensions { get; set; }

        [JsonProperty("material_quantity")]
        public object MaterialQuantity { get; set; }

        [JsonProperty("images")]
        public List<Image> Images { get; set; }

        [JsonProperty("views")]
        public long Views { get; set; }

        [JsonProperty("likes")]
        public long Likes { get; set; }

        [JsonProperty("published_at", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset PublishedAt { get; set; }

        [JsonProperty("tags")]
        public List<string> Tags { get; set; }

        [JsonProperty("licenses")]
        public List<License> Licenses { get; set; }

        [JsonProperty("filters")]
        public List<object> Filters { get; set; }

        [JsonProperty("file_mode")]
        public long FileMode { get; set; }

        [JsonProperty("files")]
        public Files Files { get; set; }

        [JsonProperty("designer")]
        public Designer Designer { get; set; }

        [JsonProperty("license")]
        public string License { get; set; }

        [JsonProperty("price")]
        public Price Price { get; set; }

        [JsonProperty("purchase_url")]
        public Uri PurchaseUrl { get; set; }

        [JsonProperty("archive_download_url")]
        public object ArchiveDownloadUrl { get; set; }
    }

    public partial class Designer
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("is_admin")]
        public bool IsAdmin { get; set; }

        [JsonProperty("is_premium")]
        public bool IsPremium { get; set; }

        [JsonProperty("is_store_manager")]
        public bool IsStoreManager { get; set; }

        [JsonProperty("is_campaign_manager")]
        public bool IsCampaignManager { get; set; }

        [JsonProperty("profile_url")]
        public Uri ProfileUrl { get; set; }

        [JsonProperty("profile_settings_url")]
        public Uri ProfileSettingsUrl { get; set; }

        [JsonProperty("avatar_url")]
        public Uri AvatarUrl { get; set; }

        [JsonProperty("avatar_thumbnail_url")]
        public Uri AvatarThumbnailUrl { get; set; }

        [JsonProperty("avatar_small_thumbnail_url")]
        public Uri AvatarSmallThumbnailUrl { get; set; }

        [JsonProperty("cover_url")]
        public Uri CoverUrl { get; set; }

        [JsonProperty("website")]
        public string Website { get; set; }

        [JsonProperty("bio")]
        public string Bio { get; set; }

        [JsonProperty("followings")]
        public long Followings { get; set; }

        [JsonProperty("followers")]
        public long Followers { get; set; }

        [JsonProperty("likes")]
        public long Likes { get; set; }

        [JsonProperty("views")]
        public long Views { get; set; }

        [JsonProperty("objects")]
        public long Objects { get; set; }

        [JsonProperty("total_prints")]
        public long TotalPrints { get; set; }

        [JsonProperty("total_collections")]
        public long TotalCollections { get; set; }

        [JsonProperty("total_objects_store")]
        public long TotalObjectsStore { get; set; }

        [JsonProperty("printing_since")]
        public object PrintingSince { get; set; }
    }

    public partial class Files
    {
        [JsonProperty("total_count")]
        public long TotalCount { get; set; }

        [JsonProperty("items")]
        public List<FilesItem> Items { get; set; }
    }

    public partial class FilesItem
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("filename")]
        public string Filename { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("status")]
        public long Status { get; set; }

        [JsonProperty("status_name")]
        public string StatusName { get; set; }

        [JsonProperty("size", NullValueHandling = NullValueHandling.Ignore)]
        public long Size { get; set; }

        [JsonProperty("patch_url")]
        public Uri PatchUrl { get; set; }

        [JsonProperty("thumbnail_url")]
        public object ThumbnailUrl { get; set; }

        [JsonProperty("download_url")]
        public object DownloadUrl { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("render360_urls")]
        public List<object> Render360Urls { get; set; }
    }

    public partial class Image
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("upload_id")]
        public string UploadId { get; set; }

        [JsonProperty("is_primary")]
        public bool IsPrimary { get; set; }

        [JsonProperty("original")]
        public Large Original { get; set; }

        [JsonProperty("tiny")]
        public Large Tiny { get; set; }

        [JsonProperty("thumbnail")]
        public Large Thumbnail { get; set; }

        [JsonProperty("standard")]
        public Large Standard { get; set; }

        [JsonProperty("large")]
        public Large Large { get; set; }
    }

    public partial class Large
    {
        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("width")]
        public long? Width { get; set; }

        [JsonProperty("height")]
        public long? Height { get; set; }
    }

    public partial class License
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("value")]
        public bool Value { get; set; }
    }
}
