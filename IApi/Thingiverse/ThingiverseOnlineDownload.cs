using ApiLinker.Generic;
using ApiLinker.Interfaces;
using ApiLinker.Thingiverse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLinker.Thingiverse
{
    public class ThingiverseOnlineDownload : OnlineImage, IDownload
    {
        private long size;

        public ThingiverseOnlineDownload(RequestDownload download)
            : base(download.Name, download.PublicUrl)
        {
            size = download.Size;
        }

        public ThingiverseOnlineDownload(RequestImage image)
            : base(image.Name, image.Sizes.First(x => x.Type == "display" && x.SizeSize == "large").Url)
        {
            size = 0;
        }

        public long Size() => size;

        public override byte[] Get() => Request.Get(Uri, new Dictionary<string, string>() { { "Authorization", ThingiverseApi.apiKey } });
        public override async Task<byte[]> GetAsync() => await Request.GetAsync(Uri, new Dictionary<string, string>() { { "Authorization", ThingiverseApi.apiKey } });
    }
}
