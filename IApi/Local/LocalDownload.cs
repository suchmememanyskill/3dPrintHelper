using ApiLinker.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace ApiLinker.Local
{
    public class LocalDownload : IDownload
    {
        public string FilePath { get; private set; }
        public LocalDownload(string filePath)
        {
            FilePath = filePath;
        }

        public string Filename() => Path.GetFileName(FilePath);
        public byte[] Get() => File.ReadAllBytes(FilePath);
        public async Task<byte[]> GetAsync() => await File.ReadAllBytesAsync(FilePath);
        public long Size() => new FileInfo(FilePath).Length;
    }
}
