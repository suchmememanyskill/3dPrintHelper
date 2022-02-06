using ApiLinker.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ApiLinker.Generic
{
    public class OnlineImage : ISavable
    {
        public string ImgFilename { get; private set; }
        public Uri Uri { get; private set; }

        public OnlineImage(string filename, Uri uri)
        {
            ImgFilename = filename;
            Uri = uri;
        }

        public OnlineImage(Uri uri)
        {
            Uri = uri;
            ImgFilename = Uri.AbsoluteUri.Split("/").Last();
        }

        public virtual byte[] Get() => Request.Get(Uri);
        public virtual async Task<byte[]> GetAsync() => await Request.GetAsync(Uri);
        public string Filename() => ImgFilename;
    }
}
