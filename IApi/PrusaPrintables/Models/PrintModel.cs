using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ApiLinker.PrusaPrintables.Models
{
    public class PrintModelData
    {
        [JsonProperty("data")]
        public PrintModelExtraData Data { get; set; }
    }

    public class PrintModelExtraData
    {
        [JsonProperty("print")]
        public PrintModel Print { get; set; }
    }
    
    public class PrintModel
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
        
        [JsonProperty("downloadCount", NullValueHandling = NullValueHandling.Ignore)]
        public long DownloadCount { get; set; }
        
        [JsonProperty("displayCount", NullValueHandling = NullValueHandling.Ignore)]
        public long DisplayCount { get; set; }
        
        [JsonProperty("likesCount", NullValueHandling = NullValueHandling.Ignore)]
        public long LikesCount { get; set; }
        
        [JsonProperty("description")]
        public string Description { get; set; }
        
        [JsonProperty("datePublished")]
        public DateTimeOffset Published { get; set; }
        
        [JsonProperty("modified")]
        public DateTimeOffset Modified { get; set; }
        
        [JsonProperty("stls")]
        public List<DownloadableModel> Models { get; set; }
        
        public Uri ToUri() => new Uri($"https://www.printables.com/model/{Id}-{Slug}");
    }

    public class DownloadableModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("filePath")]
        public string FilePath { get; set; }
        
        [JsonProperty("fileSize")]
        public long FileSize { get; set; }
        
        public Uri ToUri() => new Uri($"https://media.printables.com/{FilePath}");
    }
}