using ApiLinker.Generic;
using ApiLinker.Interfaces;
using ApiLinker.MyMiniFactory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLinker.MyMiniFactory
{
    public class MMFDownload : IDownload
    {
        private FilesItem item;
        private string referer;
        private long postId;
        public MMFDownload(FilesItem item, string referer, long postId)
        {
            this.item = item;
            this.referer = referer;
            this.postId = postId;
        }

        public string Filename() => item.Filename;
        public long Size() => item.Size;
        public byte[] Get() => Request.Get(GenerateUri(), new() {{"Referer", referer }});
        public async Task<byte[]> GetAsync() => await Request.GetAsync(GenerateUri(), new() { { "Referer", referer } });
        private Uri GenerateUri() => new Uri($"https://www.myminifactory.com/download/{postId}?downloadfile={Filename()}");
    }
}
