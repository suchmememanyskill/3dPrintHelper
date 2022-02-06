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

        // The quick action is just for local storage
        string QuickActionName()
        {
            LocalApi api = LocalApi.GetInstance();
            if (Api().ApiName() == api.ApiName())
                return "Delete locally";

            if (api.IsSaved(this))
                return "Delete locally";
            else
                return "Save locally";
        }
        async Task<QuickActionUpdateType> QuickAction()
        {
            LocalApi api = LocalApi.GetInstance();
            if (api.IsLocalInstance(this))
            {
                await api.RemoveAndSavePost(this);
                return QuickActionUpdateType.ReloadView;
            }
            else if (api.IsSaved(this))
            {
                await api.RemoveAndSavePost(this);
                return QuickActionUpdateType.UpdateModel;
            }
            else
            {
                await api.AddAndSavePost(this);
                return QuickActionUpdateType.UpdateModel;
            }
        }
    }
}
