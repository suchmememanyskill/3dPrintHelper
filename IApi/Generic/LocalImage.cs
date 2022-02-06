using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ApiLinker.Interfaces;

namespace ApiLinker.Generic
{
    public class LocalImage : ISavable
    {
        private string fullPath;
        public LocalImage(string path)
        {
            fullPath = path;
        }

        public string Filename() => Path.GetFileName(fullPath);
        public byte[] Get() => File.ReadAllBytes(fullPath);
        public async Task<byte[]> GetAsync() => await File.ReadAllBytesAsync(fullPath);

    }
}
