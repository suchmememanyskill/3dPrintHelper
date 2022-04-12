using System;
using System.Linq;
using ApiLinker.Generic;
using ApiLinker.Interfaces;
using ApiLinker.PrusaPrintables.Models;

namespace ApiLinker.PrusaPrintables
{
    public class PrintablesCreator : ICreator
    {
        private PrintablesApi api;
        private PrintablesUser creator;

        public PrintablesCreator(PrintablesApi api, PrintablesUser creator)
        {
            this.api = api;
            this.creator = creator;
        }

        public string Name() => creator.Username;

        public Uri Uri() => creator.ToUri();

        public ISavable Thumbnail() => (string.IsNullOrWhiteSpace(creator.AvatarFilePath)) ? null : new OnlineImage($"{creator.Id}_creator_thumb.{creator.AvatarFilePath.Split(".").Last()}", creator.AvatarUri());
    }
}