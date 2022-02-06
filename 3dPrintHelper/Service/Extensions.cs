using ApiLinker.Generic;
using ApiLinker.Interfaces;
using ApiLinker.Local;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3dPrintHelper.Service
{
    public static class Extensions
    {
        public static SolidColorBrush ToBrush(this Colour colour) => new(new Color(255, colour.R, colour.G, colour.B));

        public static string QuickActionName(this IPreviewPost previewPost)
        {
            LocalApi api = LocalApi.GetInstance();
            if (previewPost.Api().ApiName() == api.ApiName())
                return "Delete locally";

            if (api.IsSaved(previewPost))
                return "Delete locally";
            else
                return "Save locally";
        }

        public static async Task<QuickActionUpdateType> QuickAction(this IPreviewPost previewPost)
        {
            LocalApi api = LocalApi.GetInstance();
            if (api.IsSaved(previewPost))
            {
                QuickActionUpdateType response = api.IsLocalInstance(previewPost) ? QuickActionUpdateType.ReloadView : QuickActionUpdateType.UpdateModel;
                if ((await Utils.CreateMessageBox("Delete post", $"Are you sure you want to delete the post\n{previewPost.Name()}?    ", MessageBox.Avalonia.Enums.ButtonEnum.YesNo).Show()) != MessageBox.Avalonia.Enums.ButtonResult.Yes)
                    return QuickActionUpdateType.Nothing;

                await api.RemoveAndSavePost(previewPost);
                return response;
            }
            else
            {
                await api.AddAndSavePost(previewPost);
                return QuickActionUpdateType.UpdateModel;
            }
        }
    }
}
