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
    public class MMFCreator : ICreator
    {
        private string name;
        private ISavable thumbnail;
        private Uri uri;

        public MMFCreator(Hit hit)
        {
            name = hit.UserName;
            thumbnail = new OnlineImage(hit.UserImg);
            uri = hit.UserUrl;
        }

        public string Name() => name;

        public ISavable Thumbnail() => thumbnail;

        public Uri Uri() => uri;
    }
}
