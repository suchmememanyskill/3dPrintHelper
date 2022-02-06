using ApiLinker.Generic;
using ApiLinker.Interfaces;
using ApiLinker.Thingiverse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ApiLinker.Thingiverse
{
    public class ThingiversePreviewPost : IPreviewPost
    {
        private ThingiverseApi api;
        private Hit hit;
        private ThingiversePost fullPost;

        public ThingiversePreviewPost(ThingiverseApi api, Hit hit)
        {
            this.api = api;
            this.hit = hit;
        }

        public IApi Api() => api;

        public ICreator Creator() => new ThingiverseCreator(api, hit.Creator);

        public async Task<IPost> FullPost()
        {
            if (fullPost != null)
                return fullPost;

            string response = await api.MakeRequest(hit.Url.AbsoluteUri);
            RequestSpecificThing parsedResponse = JsonConvert.DeserializeObject<RequestSpecificThing>(response);
            fullPost = new ThingiversePost(api, parsedResponse, this);
            return fullPost;
        }

        public string Name() => hit.Name;

        public ISavable Thumbnail() => new OnlineImage($"{hit.Id}_post_thumb.{hit.Thumbnail.AbsoluteUri.Split(".").Last()}", hit.Thumbnail);

        public Uri Uri() => hit.PublicUrl;
    }
}
