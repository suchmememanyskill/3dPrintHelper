using ApiLinker.Generic;
using ApiLinker.Interfaces;
using ApiLinker.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLinker.Interfaces
{
    public interface IPreviewPost
    {
        string Name();
        ISavable Thumbnail();
        Uri Uri();
        ICreator Creator();
        Task<IPost> FullPost();
        IApi Api();
    }
}
