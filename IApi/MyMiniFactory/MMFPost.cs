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
    public class MMFPost : IPost
    {
        private FetchSpecificObject hit;
        private MMFApi api;
        private MMFPreviewPost preview;
        public MMFPost(FetchSpecificObject hit, MMFApi api, MMFPreviewPost preview)
        {
            this.hit = hit;
            this.api = api;
            this.preview = preview;
        }

        public DateTimeOffset Added() => hit.PublishedAt;
        public DateTimeOffset Modified() => Added();
        public IApi Api() => api;
        public ICreator Creator() => preview.Creator();
        public string Description() => hit.Description;
        public long DownloadCount() => hit.Views;
        public long LikeCount() => hit.Likes;
        public string Name() => hit.Name;
        public IPreviewPost PreviewPost() => preview;
        public Uri Uri() => hit.Url;

        public async Task<List<IDownload>> Downloads() => hit.Files.Items.Select(x => (IDownload)new MMFDownload(x, Uri().AbsoluteUri, hit.Id)).ToList();
        public async Task<List<ISavable>> Images() => hit.Images.Select(x => (ISavable)new OnlineImage(x.Standard.Url)).ToList();
    }
}
