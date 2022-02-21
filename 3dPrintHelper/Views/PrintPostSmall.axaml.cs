using ApiLinker.Interfaces;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using _3dPrintHelper.Service;
using System.Net;
using System.IO;
using _3dPrintHelper.ViewsExt;
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

        public PrintPostSmall()
        {
            InitializeComponent();
        }

        public PrintPostSmall(IPreviewPost post, MainView view)
        {
            this.post = post;
            MainView = view;
            InitializeComponent();
            SetControls();
            InitialiseData();
        }

        public void Update()
        {
            if (post == null)
                return;

            Panel!.Background = post.Api().ApiColour().ToBrush();
            Title!.Content = post.Name();
        }

        public async void DownloadImage()
        {
            if (post == null)
                return;

            byte[] data = await post.Thumbnail().GetAsync();
            Stream stream = new MemoryStream(data);
            Background!.Source = Avalonia.Media.Imaging.Bitmap.DecodeToWidth(stream, 300);
        }

        public void SetButtonRowVisibility(bool visible) => ButtonRow.IsVisible = visible;

        public void UpdateQuickAction()
        {
            QuickAction.Content = post!.QuickActionName();
        }

        public void SetInfoButtonState(bool busy)
        {
            FullView.IsEnabled = !busy;

            if (busy)
                FullView.Content = "Busy...";
            else
                FullView.Content = "Info";
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void InitialiseData()
        {
            FullView.Background = post!.Api().ApiColour().ToBrush();
            QuickAction.Background = post!.Api().ApiColour().ToBrush();

            SetButtonRowVisibility(false);
            UpdateQuickAction();
            Update();
            Dispatcher.UIThread.Post(DownloadImage, DispatcherPriority.Background);
        }

        [Command(nameof(FullView))]
        public async void SwitchView()
        {
            SetInfoButtonState(true);
            MainView.SetOverlay(new PrintPost(await post!.FullPost(), this));
        }
        
        [Command(nameof(QuickAction))]
        public async void OnQuickAction()
        {
            QuickAction.Content = "Busy...";
            QuickAction.IsEnabled = false;
            if (await post!.QuickAction() == ApiLinker.Generic.QuickActionUpdateType.ReloadView)
                await MainView.UpdateApiViewTask();
            else
            {
                UpdateQuickAction();
                QuickAction.IsEnabled = true;
            }
        }
    }
}