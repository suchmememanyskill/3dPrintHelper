using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ApiLinker.Interfaces
{
    public interface IDownload : ISavable
    {
        long Size();

        string SizeReadable()
        {
            int type = 0;
            double bytesLeft = Size();
            while (bytesLeft >= 1024)
            {
                type++;
                bytesLeft /= 1024;
            }
            string[] gameSizes = { "B", "KB", "MB", "GB" };

            return $"{bytesLeft:0.00} {gameSizes[type]}";
        }

        bool IsModel() => new List<string>() { ".stl", ".obj", ".3mf" }.Any(x => Filename().ToLower().EndsWith(x));
    }
}
