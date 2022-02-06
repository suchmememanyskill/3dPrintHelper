using ApiLinker.Interfaces;
using ApiLinker.Thingiverse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ApiLinker.Generic;

namespace ApiLinker.Thingiverse
{
    public class ThingiverseDownload : IDownload
    {
        private ThingiverseApi api;
        private RequestDownload download;
        public ThingiverseDownload(ThingiverseApi api, RequestDownload download)
        {
            this.api = api;
            this.download = download;
        }

        public string Filename() => download.Name;

        public byte[] Get() => Request.Get(Uri());

        public async Task<byte[]> GetAsync() => await Request.GetAsync(Uri());

        public long Size() => download.Size;

        public Uri Uri() => download.PublicUrl;
    }
}
