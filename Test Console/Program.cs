using ApiLinker.Generic;
using ApiLinker.Interfaces;
using ApiLinker.Thingiverse;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Test_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            ThingiverseApi api = new ThingiverseApi();
            //List<IPreviewPost> posts = api.GetPosts("Popular Last 7 Days", 1, 0);
            List<IPreviewPost> posts = api.GetPostsBySearch("test+benchy", 1, 0);
            IPost post = posts[0].FullPost();
            List<OnlineImage> img = post.Images();
            Console.WriteLine(post.Name());
            //downloads.First().DownloadToDir(".");
        }
    }
}
