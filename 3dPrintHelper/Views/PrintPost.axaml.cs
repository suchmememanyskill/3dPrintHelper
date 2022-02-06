using _3dPrintHelper.Service;
using ApiLinker.Interfaces;
using ApiLinker.Local;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Linq;

namespace _3dPrintHelper.Views
{
    public partial class PrintPost : UserControl
    {
        private StackPanel topPanel;
        private Label title;
        private Label creator;
        private Image img;
        private Button leftArrow;
        private Button rightArrow;
        private Button back;
        private Button openFolder;
        private Button openUrl;
        private Button openPrusa;

        private IPost post;
        private MainView view;
        private PrintPostSmall small;
        private LocalApi localApi = LocalApi.GetInstance();
        private List<ISavable> savables;
        private int currentIndex = 0;

        public PrintPost(IPost post, PrintPostSmall small)
        {
            this.post = post;
            this.small = small;
            this.view = small?.MainView;
            InitializeComponent();
        }

        public PrintPost() : this(null, null) { }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            topPanel = this.FindControl<StackPanel>("TopPanel");
            title = this.FindControl<Label>("Title");
            creator = this.FindControl<Label>("Creator");
            img = this.FindControl<Image>("Img");
            leftArrow = this.FindControl<Button>("LeftArrow");
            rightArrow = this.FindControl<Button>("RightArrow");
            back = this.FindControl<Button>("Back");
            openFolder = this.FindControl<Button>("OpenFolder");
            openUrl = this.FindControl<Button>("OpenUrl");
            openPrusa = this.FindControl<Button>("OpenPrusa");

            title.Content = post?.Name();
            creator.Content = "By " + post?.Creator().Name();

            topPanel.Background = post?.Api().ApiColour().ToBrush();
            Dispatcher.UIThread.Post(LoadImages);

            leftArrow.Command = new LambdaCommand(x => Dispatcher.UIThread.Post(Left));
            rightArrow.Command = new LambdaCommand(x => Dispatcher.UIThread.Post(Right));
            openFolder.Command = new LambdaCommand(x => Dispatcher.UIThread.Post(OpenFiles));
            openUrl.Command = new LambdaCommand(x => Utils.OpenUrl(post!.Uri().AbsoluteUri));
            openPrusa.Command = new LambdaCommand(x => Dispatcher.UIThread.Post(LoadPrusa));
            back.Command = new LambdaCommand(x => Back());
        }

        private void SetAllButtonsEnabled(bool enabled) => openPrusa.IsEnabled = openFolder.IsEnabled = enabled;

        private async Task<LocalPost> GetLocalPost()
        {
            LocalPost localPost = localApi.FindLocalPost(post.PreviewPost());
            if (localPost != null)
                return localPost;

            // We're gonna assume it's not downloaded
            await post.PreviewPost().QuickAction();
            return localApi.FindLocalPost(post.PreviewPost());
        }

        private async void OpenFiles()
        {
            SetAllButtonsEnabled(false);
            LocalPost localPost = await GetLocalPost();
            Utils.OpenFolder(localPost.FilePath());
            SetAllButtonsEnabled(true);
        }

        private async void LoadPrusa()
        {
            SetAllButtonsEnabled(false);
            LocalPost localPost = await GetLocalPost();
            List<IDownload> files = (await localPost.Downloads()).Where(x => x.IsModel()).ToList();
            List<string> paths = files.Select(x => Path.Join(localPost.FilePath(), x.Filename())).ToList();
            Process.Start("C:/Program Files/Prusa3D/PrusaSlicer/prusa-slicer.exe", string.Join(" ", paths));
            SetAllButtonsEnabled(true);
        }

        private async void LoadImages()
        {
            savables = await post?.Images();
            await UpdateImageTask();
        }

        private async Task UpdateImageTask()
        {
            if (savables == null)
                return;

            byte[] data = await savables[currentIndex].GetAsync();
            Stream stream = new MemoryStream(data);
            img!.Source = Avalonia.Media.Imaging.Bitmap.DecodeToWidth(stream, 700);
        }

        private async void Left()
        {
            img!.Source = null;
            currentIndex = (currentIndex - 1) % savables.Count;
            if (currentIndex < 0)
                currentIndex = savables.Count - 1;
            await UpdateImageTask();
        }

        private async void Right()
        {
            img!.Source = null;
            currentIndex = (currentIndex + 1) % savables.Count;
            await UpdateImageTask();
        }

        private async void UpdateImage() => await UpdateImageTask();

        private void Back()
        {
            small.UpdateQuickAction();
            small.SetInfoButtonState(false);
            view.SetOverlay(null);
        }
    }
}
