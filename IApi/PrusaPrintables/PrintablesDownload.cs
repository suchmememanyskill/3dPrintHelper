using System;
using System.Threading.Tasks;
using ApiLinker.Generic;
using ApiLinker.Interfaces;
using ApiLinker.PrusaPrintables.Models;

namespace ApiLinker.PrusaPrintables
{
    public class PrintablesDownload : IDownload
    {
        private DownloadableModel model;

        public PrintablesDownload(DownloadableModel model)
        {
            this.model = model;
        }

        public string Filename() => model.Name;
        public byte[] Get() => Request.Get(Uri());
        public async Task<byte[]> GetAsync() => await Request.GetAsync(Uri());
        public long Size() => model.FileSize;
        public Uri Uri() => model.ToUri();
    }
}