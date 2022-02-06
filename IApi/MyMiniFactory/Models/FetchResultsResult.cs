using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiLinker.MyMiniFactory.Models
{
    public class FetchResultsResult
    {
        [JsonProperty("objectResults")]
        public List<Hit> ObjectResults { get; set; }
    }

    public class Hit
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("price")]
        public Price Price { get; set; }

        [JsonProperty("document_name_s")]
        public string DocumentNameS { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("name_acc")]
        public string NameAcc { get; set; }

        [JsonProperty("folderURL")]
        public Uri FolderUrl { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("is_manufacturable")]
        public long IsManufacturable { get; set; }

        [JsonProperty("manufacturable_price")]
        public long ManufacturablePrice { get; set; }

        [JsonProperty("user_name")]
        public string UserName { get; set; }

        [JsonProperty("user_username")]
        public string UserUsername { get; set; }

        [JsonProperty("user_img")]
        public Uri UserImg { get; set; }

        [JsonProperty("obj_img")]
        public Uri ObjImg { get; set; }

        [JsonProperty("status")]
        public long Status { get; set; }

        [JsonProperty("visibility")]
        public long Visibility { get; set; }

        [JsonProperty("featured")]
        public long Featured { get; set; }

        [JsonProperty("visits")]
        public long Visits { get; set; }

        [JsonProperty("position")]
        public long Position { get; set; }

        [JsonProperty("date_published")]
        public DateTimeOffset DatePublished { get; set; }

        [JsonProperty("license_store")]
        public long LicenseStore { get; set; }

        [JsonProperty("absolute_url")]
        public Uri AbsoluteUrl { get; set; }

        [JsonProperty("user_url")]
        public Uri UserUrl { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    public class Price
    {
        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("value")]
        public double Value { get; set; }

        public bool IsFree
        {
            get
            {
                return Value == 0;
            }
        }
    }
}
