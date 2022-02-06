using ApiLinker.Generic;
using ApiLinker.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace ApiLinker.Local
{
    public class LocalCreator : ICreator
    {
        [JsonIgnore]
        public LocalApi LocalApi { get; set; }
        public string LocalName { get; set; }
        public string LocalCreatorThumbnailPath { get; set; }
        public Uri Url { get; set; }

        public LocalCreator() { }
        public LocalCreator(LocalApi api) => LocalApi = api;

        public string Name() => LocalName;
        public ISavable Thumbnail() => new LocalImage(Path.Join(LocalApi.basePath, LocalCreatorThumbnailPath));
        public Uri Uri() => Url;

        public async Task ApplyCreatorAsync(ICreator creator, long id)
        {
            LocalName = creator.Name();
            Url = creator.Uri();
            LocalCreatorThumbnailPath = Path.Join(id.ToString(), "creator_thumb.jpg");
            Directory.CreateDirectory(Path.Join(LocalApi.basePath, id.ToString()));
            string imgPath = Path.Join(LocalApi.basePath, id.ToString(), "creator_thumb.jpg");
            await creator.Thumbnail().SaveAsync(imgPath);
        }
    }
}
