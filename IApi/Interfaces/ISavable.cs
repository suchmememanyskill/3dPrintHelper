using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ApiLinker.Generic;

namespace ApiLinker.Interfaces
{
    public interface ISavable
    {
        string Filename();
        byte[] Get();
        Task<byte[]> GetAsync();

        void Save(string path) => File.WriteAllBytes(path, Get());
        async Task SaveAsync(string path) => await File.WriteAllBytesAsync(path, await GetAsync());
    }
}
