using ApiLinker.Generic;
using ApiLinker.Interfaces;
using ApiLinker.MyMiniFactory.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLinker.MyMiniFactory
{
    public class MMFPreviewPost : IPreviewPost
    {
        private Hit hit;
        private MMFApi api;
        public MMFPreviewPost(Hit hit, MMFApi api)
        {
            this.hit = hit;
            this.api = api;
        }

        public IApi Api() => api;
        public ICreator Creator() => new MMFCreator(hit);
        public string Name() => hit.Name;
        public Uri Uri() => hit.AbsoluteUrl;
        public ISavable Thumbnail() => new OnlineImage(hit.ObjImg);

        public async Task<IPost> FullPost()
        {
            string url = $"https://www.myminifactory.com/api/v2/objects/{hit.Id}?key={MMFApi.apiKey}";
            string response = await Request.GetStringAsync(new Uri(url));
            FetchSpecificObject result = JsonConvert.DeserializeObject<FetchSpecificObject>(response);
            return new MMFPost(result, api, this);
        }
    }
}
