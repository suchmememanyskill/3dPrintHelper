using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ApiLinker.PrusaPrintables.Models
{
    public class PrintList
    {
        [JsonProperty("data")]
        public PrintListData Data { get; set; }
    }

    public class PrintListData
    {
        [JsonProperty("morePrints")]
        public MorePrints MorePrints { get; set; }
        
        [JsonProperty("result")]
        public SearchResults SearchResult { get; set; }
    }

    public class MorePrints
    {
        [JsonProperty("cursor")]
        public string Cursor { get; set; }
        
        [JsonProperty("items")]
        public List<PrintablesItem> Items { get; set; }
    }

    public class SearchResults
    {
        public List<PrintableItemSearch> Items { get; set; }
    }

    public class PrintablesItem
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("slug")]
        public string Slug { get; set; }
        
        [JsonProperty("images")]
        public List<PrintablesImage> Images { get; set; }
        
        [JsonProperty("user")]
        public PrintablesUser User { get; set; }
        
        public Uri ToUri() => new Uri($"https://www.printables.com/model/{Id}-{Slug}");
        
    }

    public class PrintableItemSearch : PrintablesItem
    {
        [JsonProperty("mainImage")]
        public string MainThumbnail { get; set; }
        
        public Uri ToSearchMainThumbnaiUri() => new Uri($"https://media.printables.com/{MainThumbnail}");
    }

    public class PrintablesImage
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [JsonProperty("filePath")]
        public string FilePath { get; set; }

        public Uri ToUri() => new Uri($"https://media.printables.com/{FilePath}");
    }

    public class PrintablesUser
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [JsonProperty("publicUsername")]
        public string Username { get; set; }
        
        [JsonProperty("slug")]
        public string Slug { get; set; }
        
        [JsonProperty("avatarFilePath")]
        public string AvatarFilePath { get; set; }
        
        public Uri ToUri() => new Uri($"https://www.printables.com/social/{Id}-{Slug}/about");
        public Uri AvatarUri() => new Uri($"https://media.printables.com/{AvatarFilePath}");
    }
}