using ApiLinker.Generic;
using ApiLinker.Interfaces;
using ApiLinker.Thingiverse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ApiLinker.Thingiverse
{
    public class ThingiverseApi : IApi
    {
        public static string apiKey = "Bearer 56edfc79ecf25922b98202dd79a291aa";
        private long lastRequestLen = -1;

        private readonly Dictionary<string, string> sortTypes = new()
        {
            { "Popular Last 7 Days", "sort=popular&posted_after=now-7d" },
            { "Popular Last 30 Days", "sort=popular&posted_after=now-30d" },
            { "Popular This Year", "sort=popular&posted_after=now-365d" },
            { "Popular All Time", "sort=popular" },
            { "Newest", "sort=newest" },
            { "Most Makes", "sort=makes" },
        };

        public Colour ApiColour() => new(3, 89, 181);
        public string ApiName() => "Thingiverse";
        public List<string> SortTypes() => sortTypes.Keys.ToList();

        public async Task<string> MakeRequest(string url)
        {
            using (var client = new WebClient())
            {
                client.Headers["Authorization"] = apiKey;
                return await client.DownloadStringTaskAsync(url);
            }
        }

        public async Task<List<IPreviewPost>> GetPosts(string sortType, int amount, int skip)
        {
            string response = await MakeRequest($"https://api.thingiverse.com/search/?page={skip / amount + 1}&per_page={amount}&{sortTypes[sortType]}&type=things");
            RequestThings parsedResponse = JsonConvert.DeserializeObject<RequestThings>(response);
            lastRequestLen = parsedResponse.Total;
            return parsedResponse.Hits.Select(x => (IPreviewPost)new ThingiversePreviewPost(this, x)).ToList();
        }

        public async Task<List<IPreviewPost>> GetPostsBySearch(string search, int amount, int skip)
        {
            string response = await MakeRequest($"https://api.thingiverse.com/search/{search}?page={skip / amount + 1}&per_page={amount}&sort=relevant&type=things");
            RequestThings parsedResponse = JsonConvert.DeserializeObject<RequestThings>(response);
            lastRequestLen = parsedResponse.Total;
            return parsedResponse.Hits.Select(x => (IPreviewPost)new ThingiversePreviewPost(this, x)).ToList();
        }
    }
}
