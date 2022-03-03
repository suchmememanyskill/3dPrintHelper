using ApiLinker.Generic;
using ApiLinker.Interfaces;
using ApiLinker.Thingiverse;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ApiLinker;
using ApiLinker.Local;
using ApiLinker.Thingiverse.Models;
using Newtonsoft.Json;

namespace Test_Console
{
    public static class ThingiverseApiExt
    {
        public static List<IPreviewPost> ParsePosts(this ThingiverseApi api, string data)
        {
            var parsedResponse = JsonConvert.DeserializeObject<List<Hit>>(data);
            
            return parsedResponse.Select(x => (IPreviewPost)new ThingiversePreviewPost(api, x)).ToList();
        }
    }
    
    class Program
    {
        static async Task ImportThingiverseJson(string data)
        {
            ThingiverseApi thingiverseApi = new();
            LocalApi local = LocalApi.GetInstance();

            var list = thingiverseApi.ParsePosts(data);
            foreach (var previewPost in list)
            {
                if (!local.IsSaved(previewPost))
                {
                    Console.WriteLine("Saving " + previewPost.Name());
                    await local.AddAndSavePost(previewPost);
                }
            }
        }
        
        static void Main(string[] args)
        {
            string data = File.ReadAllText("./data.json");
            ImportThingiverseJson(data).GetAwaiter().GetResult();

            /*
            Console.WriteLine("Hello World!");
            ThingiverseApi api = new ThingiverseApi();
            //List<IPreviewPost> posts = api.GetPosts("Popular Last 7 Days", 1, 0);
            List<IPreviewPost> posts = api.GetPostsBySearch("test+benchy", 1, 0);
            IPost post = posts[0].FullPost();
            List<OnlineImage> img = post.Images();
            Console.WriteLine(post.Name());
            //downloads.First().DownloadToDir(".");
            */
        }
    }
}
