using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiLinker.Generic;
using ApiLinker.Interfaces;
using ApiLinker.PrusaPrintables.Models;

namespace ApiLinker.PrusaPrintables
{
    public class PrintablesPost : IPost
    {
        private PrintablesApi api;
        private PrintModel post;
        private PrintablesPreviewPost previewPost;
        
        public PrintablesPost(PrintablesApi api, PrintModel post, PrintablesPreviewPost previewPost)
        {
            this.post = post;
            this.api = api;
            this.previewPost = previewPost;
        }

        public string Name() => post.Name;

        public string Description() => post.Description;

        public async Task<List<ISavable>> Images() => post.Images.Select(x => (ISavable)new OnlineImage(x.ToUri())).ToList();

        public Uri Uri() => post.ToUri();

        public ICreator Creator() => new PrintablesCreator(api, post.User);

        public DateTimeOffset Added() => post.Published;

        public DateTimeOffset Modified() => post.Modified;

        public long DownloadCount() => post.DownloadCount;

        public long LikeCount() => post.LikesCount;

        public async Task<List<IDownload>> Downloads() =>
            post.Models.Select(x => (IDownload) new PrintablesDownload(x)).ToList();

        public IApi Api() => api;

        public IPreviewPost PreviewPost() => previewPost;
    }
}