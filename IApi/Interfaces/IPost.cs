using ApiLinker.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLinker.Interfaces
{
    public interface IPost
    {
        string Name();
        string Description();
        Task<List<ISavable>> Images();
        Uri Uri();
        ICreator Creator();
        DateTimeOffset Added();
        DateTimeOffset Modified();
        long DownloadCount();
        long LikeCount();
        Task<List<IDownload>> Downloads();
        IApi Api();
        IPreviewPost PreviewPost();
    }

}
