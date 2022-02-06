using ApiLinker.Generic;
using ApiLinker.Interfaces;
using ApiLinker.Thingiverse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLinker.Thingiverse
{
    public class ThingiverseCreator : ICreator
    {
        private ThingiverseApi api;
        private Creator creator;

        public ThingiverseCreator(ThingiverseApi api, Creator creator)
        {
            this.api = api;
            this.creator = creator;
        }

        public string Name() => creator.Name;

        public ISavable Thumbnail() => new OnlineImage($"{creator.Id}_creator_thumb.{creator.Thumbnail.AbsoluteUri.Split(".").Last()}", creator.Thumbnail);

        public Uri Uri() => creator.PublicUrl;
    }
}
