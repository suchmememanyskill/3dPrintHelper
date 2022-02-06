using ApiLinker.Generic;
using ApiLinker.Local;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiLinker.Interfaces
{
    public interface IApi
    {
        string ApiName();
        Colour ApiColour();
        List<string> SortTypes();
        Task<List<IPreviewPost>> GetPosts(string sortType, int amount, int skip);
        Task<List<IPreviewPost>> GetPostsBySearch(string search, int amount, int skip);
        long GetItemCountOnLastRequest() => -1;
    }
}
