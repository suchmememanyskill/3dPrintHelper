using ApiLinker.Interfaces;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using _3dPrintHelper.Service;
using System.Net;
using System.IO;
using _3dPrintHelper.ViewsExt;
using Avalonia.Media.Imaging;
using Avalonia.Threading;

namespace _3dPrintHelper.Views
{
    public partial class PrintPostSmall : UserControlExt<PrintPostSmall>
    {
        private IPreviewPost? post;
        public MainView MainView { get; }
        [NamedControl] public DockPanel Panel { get; set; }
        [NamedControl] public Label Title { get; set; }
        [NamedControl] public Image Background { get; set; }
        [NamedControl] public StackPanel ButtonRow { get; set; }
        [NamedControl] public Button QuickAction { get; set; }
        [NamedControl] public Button FullView { get; set; }
        
        [Binding(nameof(Panel), "Background")] 
        [Binding(nameof(FullView), "Background")]
        [Binding(nameof(QuickAction), "Background")]
        public SolidColorBrush? Brush => post?.Api().ApiColour().ToBrush();

        [Binding(nameof(Title), "Content")]
        public string? PostName => post?.Name();

        [Binding(nameof(FullView), "Content")]
        public string CurrentFullViewState => (FullView.IsEnabled) ? "Info" : "Busy...";

        [Binding(nameof(QuickAction), "Content")]
        public string CurrentQuickActionState => (QuickAction.IsEnabled) ? post!.QuickActionName() : "Busy...";

        public PrintPostSmall() => InitializeComponent();

        public PrintPostSmall(IPreviewPost post, MainView view)
        {
            this.post = post;
            MainView = view;
            InitializeComponent();
            SetControls();
            InitialiseData();
            UpdateView();
        }

        public async void DownloadImage()
        {
            if (post == null)
                return;

            ISavable savable = post.Thumbnail();

            if (savable == null)
                return;

            byte[] data = await savable.GetAsync();
            if (data != null)
            {
                Stream stream = new MemoryStream(data);
                Background!.Source = Bitmap.DecodeToWidth(stream, 300);
            }
        }

        public void SetButtonRowVisibility(bool visible) => ButtonRow.IsVisible = visible;

        public void SetInfoButtonState(bool busy)
        {
            FullView.IsEnabled = !busy;
            UpdateView();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void InitialiseData()
        {
            SetButtonRowVisibility(false);
            Dispatcher.UIThread.Post(DownloadImage, DispatcherPriority.Background);
        }

        [Command(nameof(FullView))]
        public async void SwitchView()
        {
            SetInfoButtonState(true);
            IPost iPost = await post!.FullPost();
            if (iPost != null)
                MainView.SetOverlay(new PrintPost(iPost, this));
        }
        
        [Command(nameof(QuickAction))]
        public async void OnQuickAction()
        {
            QuickAction.IsEnabled = false;
            UpdateView();
            if (await post!.QuickAction() == ApiLinker.Generic.QuickActionUpdateType.ReloadView)
                await MainView.UpdateApiViewTask();
            else
            {
                QuickAction.IsEnabled = true;
                UpdateView();
            }
        }
    }
}