using System;
using System.Linq;
using System.Threading.Tasks;
using ApiLinker.Generic;
using ApiLinker.Interfaces;
using ApiLinker.PrusaPrintables.Models;

namespace ApiLinker.PrusaPrintables
{
    public class PrintablesPreviewPost : IPreviewPost
    {
        private PrintablesApi api;
        private PrintablesItem previewPost;
        
        public PrintablesPreviewPost(PrintablesApi api, PrintablesItem item)
        {
            previewPost = item;
            this.api = api;
        }

        public string Id() => previewPost.Id;
        
        public string Name() => previewPost.Name;

        public virtual ISavable Thumbnail()
        {
            if (previewPost.Images == null)
                return null;
            
            string ext = previewPost!.Images?.First()?.FilePath?.Split(".")?.Last() ?? "jpg";
            return new OnlineImage($"{previewPost.Id}_post_thumb.{ext}", previewPost.Images.First().ToUri());
        }

        public Uri Uri() => previewPost.ToUri();

        public ICreator Creator() => new PrintablesCreator(api, previewPost.User);

        public async Task<IPost> FullPost() => await api.GetPost(this);

        public IApi Api() => api;
    }

    public class PrintablesSearchPreviewPost : PrintablesPreviewPost
    {
        private PrintableItemSearch item;
        
        public PrintablesSearchPreviewPost(PrintablesApi api, PrintableItemSearch item) : base(api, item)
        {
            this.item = item;
        }

        public override ISavable Thumbnail()
        {
            string ext = item.MainThumbnail.Split(".")?.Last() ?? "jpg";
            return new OnlineImage($"{item.Id}_post_thumb.{ext}", item.ToSearchMainThumbnaiUri());
        }
    }
}