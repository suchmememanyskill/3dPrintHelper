using ApiLinker.Generic;
using ApiLinker.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ApiLinker.Local.Models;
using Newtonsoft.Json;

namespace ApiLinker.Local
{
    public class LocalApi : IApi 
    {
        public readonly string basePath = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Local3dFiles");
        private static LocalApi instance = null;
        private LocalRoot root;
        private LocalApi() => Load();
        public static LocalApi GetInstance()
        {
            if (instance == null)
                instance = new();

            return instance;
        }

        public Colour ApiColour() => new(255, 69, 0);

        public string ApiName() => "Local";

        public long GetItemCountOnLastRequest() => root.Posts.Count;

        public async Task<List<IPreviewPost>> GetPosts(string sortType, int amount, int skip)
        {
            switch (sortType)
            {
                case "Alphabetically":
                    return root.Posts.OrderBy(x => x.Name()).Select(x => (IPreviewPost)x).Skip(skip).Take(amount).ToList();
                case "Date Added":
                    return root.Posts.OrderByDescending(x => x.Added()).Select(x => (IPreviewPost)x).Skip(skip).Take(amount).ToList();
                default:
                    throw new NotImplementedException();
            }
        }

        public async Task<List<IPreviewPost>> GetPostsBySearch(string search, int amount, int skip)
        {
            string searchLower = search.ToLower();
            return root.Posts.Where(x => x.Name().ToLower().Contains(searchLower) || x.Description().ToLower().Contains(searchLower)).Select(x => (IPreviewPost)x).Skip(skip).Take(amount).ToList();
        }

        public List<string> SortTypes() => new List<string>() { "Alphabetically", "Date Added" };

        public async Task AddAndSavePost(IPreviewPost post)
        {
            Load(); // Make sure our data is up to date before writing to it
            LocalPost logicalPost = new(this);
            IPost iPost = await post.FullPost();

            if (iPost == null)
                return;
            
            await logicalPost.ApplyPost(post, iPost);
            root.Posts.Add(logicalPost);
            Save();
        }

        public LocalPost FindLocalPost(IPreviewPost post) => root.Posts.Find(x => x.Name() == post.Name() && x.Creator().Name() == post.Creator().Name());

        public async Task RemoveAndSavePost(IPreviewPost post)
        {
            Load(); // Make sure our data is up to date before writing to it
            LocalPost found = FindLocalPost(post);
            if (found != null)
            {
                Directory.Delete(Path.Join(basePath, found.Id.ToString()), true);
                root.Posts.Remove(found);
                Save();
            }
        }

        public bool IsSaved(IPreviewPost post) => root.Posts.Any(x => x.Name() == post.Name() && x.Creator().Name() == post.Creator().Name());
        public bool IsLocalInstance(IPreviewPost post) => root.Posts.Contains(post);

        private void Save()
        {
            Directory.CreateDirectory(basePath);
            string full = JsonConvert.SerializeObject(root);
            File.WriteAllText(Path.Join(basePath, "installed.json"), full);
        }

        private void Load()
        {
            string path = Path.Join(basePath, "installed.json");
            if (File.Exists(path))
            {
                root = JsonConvert.DeserializeObject<LocalRoot>(File.ReadAllText(path));
                root.Posts.ForEach(x => { x.LocalApi = this; x.LocalCreator.LocalApi = this; });
            }
            else
                root = new();
        }
    }
}
