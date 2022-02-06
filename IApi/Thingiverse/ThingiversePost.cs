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
    public class ThingiversePost : IPost
    {
        private ThingiverseApi api;
        private RequestSpecificThing hit;
        private ThingiversePreviewPost preview;

        public ThingiversePost(ThingiverseApi api, RequestSpecificThing hit, ThingiversePreviewPost preview)
        {
            this.api = api;
            this.hit = hit;
            this.preview = preview;
        }

        public DateTimeOffset Added() => hit.Added;

        public IApi Api() => api;

        public ICreator Creator() => new ThingiverseCreator(api, hit.Creator);

        public string Description() => hit.Description;

        public long DownloadCount() => hit.DownloadCount;

        public async Task<List<IDownload>> Downloads()
        {
            string response = await api.MakeRequest(hit.FilesUrl.AbsoluteUri);
            List<RequestDownload> parsedResponse = JsonConvert.DeserializeObject<List<RequestDownload>>(response);
            return parsedResponse.Select(x => (IDownload)new ThingiverseOnlineDownload(x)).ToList();
        }

        private List<ISavable> requestImageCache;
        public async Task<List<ISavable>> Images()
        {
            if (requestImageCache != null)
                return requestImageCache;

            string response = await api.MakeRequest(hit.ImagesUrl.AbsoluteUri);
            List<RequestImage> parsedResponse = JsonConvert.DeserializeObject<List<RequestImage>>(response);
            requestImageCache = parsedResponse.Select(x => (ISavable)new ThingiverseOnlineDownload(x)).ToList();
            return requestImageCache;
        }

        public long LikeCount() => hit.LikeCount;

        public DateTimeOffset Modified() => hit.Modified;

        public string Name() => hit.Name;

        public IPreviewPost PreviewPost() => preview;

        public Uri Uri() => hit.PublicUrl;
    }
}
