using ApiLinker.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ApiLinker.Generic;
using Newtonsoft.Json;

namespace ApiLinker.Local
{
    public class LocalPost : IPost, IPreviewPost
    {
        private static Random random = new();

        [JsonIgnore]
        public LocalApi LocalApi { get; set; }
        public LocalPost() { }
        public LocalPost(LocalApi api) => LocalApi = api;

        public long Id { get; set; }
        public LocalCreator LocalCreator { get; set; }
        public string LocalName { get; set; }
        public string LocalDescription { get; set; }
        public long LocalDownloadCount { get; set; }
        public long LocalLikeCount { get; set; }
        public Uri Url { get; set; }

        public DateTimeOffset Added() => Directory.GetCreationTime(Path.Join(LocalApi.basePath, Id.ToString()));
        public DateTimeOffset Modified() => Added();
        public IApi Api() => LocalApi;
        public ICreator Creator() => LocalCreator;
        public string Name() => LocalName;
        public string Description() => LocalDescription;
        public long DownloadCount() => LocalDownloadCount;
        public long LikeCount() => LocalLikeCount;
        public async Task<IPost> FullPost() => this;
        public IPreviewPost PreviewPost() => this;
        public string BasePath() => Path.Join(LocalApi.basePath, Id.ToString());
        public Uri Uri() => Url;
        public string FilePath() => Path.Join(LocalApi.basePath, Id.ToString(), "Files");

        public async Task<List<IDownload>> Downloads() => Directory.GetFiles(FilePath()).ToList().Select(x => (IDownload)new LocalDownload(x)).ToList();
        public async Task<List<ISavable>> Images() => Directory.GetFiles(Path.Join(BasePath(), "Images")).ToList().Select(x => (ISavable)new LocalImage(x)).ToList();
        public ISavable Thumbnail() => new LocalImage(Path.Join(BasePath(), "post_thumb.jpg"));

        public async Task ApplyPost(IPreviewPost preview, IPost post)
        {
            Id = (long)random.Next() << 32 | (long)random.Next();
            LocalCreator = new LocalCreator(LocalApi);
            await LocalCreator.ApplyCreatorAsync(post.Creator(), Id); // Also creates dir

            await preview.Thumbnail().SaveAsync(Path.Join(BasePath(), "post_thumb.jpg"));

            LocalName = post.Name();
            LocalDescription = post.Description();
            LocalDownloadCount = post.DownloadCount();
            LocalLikeCount = post.LikeCount();
            Url = post.Uri();

            Directory.CreateDirectory(Path.Join(BasePath(), "Images"));
            foreach (ISavable img in await post.Images())
                await img.SaveAsync(Path.Join(BasePath(), "Images", img.Filename()));

            Directory.CreateDirectory(Path.Join(BasePath(), "Files"));
            foreach (ISavable file in await post.Downloads())
                await file.SaveAsync(Path.Join(BasePath(), "Files", file.Filename()));
        }
    }
}
