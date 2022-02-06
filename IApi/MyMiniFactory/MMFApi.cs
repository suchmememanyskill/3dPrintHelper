using ApiLinker.Generic;
using ApiLinker.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ApiLinker.MyMiniFactory.Models;

namespace ApiLinker.MyMiniFactory
{
    public class MMFApi : IApi
    {
        public static string apiKey = "d7c64faa-aa6e-4645-b47a-95cf3ddc991a"; // This is a personal API key. Please do not use this

        private readonly Dictionary<string, string> sortTypes = new()
        {
            { "Featured Popular", "&featured=1&sortBy=popularity" },
            { "Featured Recently Published", "&featured=1&sortBy=date" },
            { "Featured Most Viewed", "&featured=1&sortBy=visits" },
            { "Relevance", "" },
            { "Popularity", "&sortBy=popularity" },
            { "Latest Published", "&sortBy=date" },
            { "Most Viewed", "&sortBy=visits" },
        };

        public Colour ApiColour() => new Colour(0, 196, 166);

        public string ApiName() => "MyMiniFactory";

        public async Task<List<IPreviewPost>> GetPosts(string sortType, int amount, int skip)
        {
            string url = $"https://www.myminifactory.com/search/fetch_search/?object=1&page={skip / amount + 1}&store=0{sortTypes[sortType]}";
            string response = await Request.GetStringAsync(new Uri(url));
            FetchResultsResult result = JsonConvert.DeserializeObject<FetchResultsResult>(response);
            return result.ObjectResults.Select(x => (IPreviewPost)new MMFPreviewPost(x, this)).ToList();
        }

        public async Task<List<IPreviewPost>> GetPostsBySearch(string search, int amount, int skip)
        {
            string url = $"https://www.myminifactory.com/search/fetch_search/?object=1&page={skip / amount + 1}&store=0&query={search}";
            string response = await Request.GetStringAsync(new Uri(url));
            FetchResultsResult result = JsonConvert.DeserializeObject<FetchResultsResult>(response);
            return result.ObjectResults.Select(x => (IPreviewPost)new MMFPreviewPost(x, this)).ToList();
        }

        public List<string> SortTypes() => sortTypes.Keys.ToList();
    }
}
