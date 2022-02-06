using ApiLinker.Interfaces;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using _3dPrintHelper.Service;
using System.Net;
using System.IO;
using Avalonia.Threading;

namespace _3dPrintHelper.Views
{
    public partial class PrintPostSmall : UserControl
    {
        private IPreviewPost? post;
        public MainView MainView { get; }
        private DockPanel? panel;
        private Label? title;
        private Avalonia.Controls.Image? image;
        private StackPanel buttonRow;
        private Button quickAction;
        private Button fullView;

        public PrintPostSmall()
        {
            InitializeComponent();
        }

        public PrintPostSmall(IPreviewPost post, MainView view)
        {
            this.post = post;
            MainView = view;
            InitializeComponent();
        }

        public void Update()
        {
            if (post == null)
                return;

            panel!.Background = post.Api().ApiColour().ToBrush();
            title!.Content = post.Name();
        }

        public async void DownloadImage()
        {
            if (post == null)
                return;

            byte[] data = await post.Thumbnail().GetAsync();
            Stream stream = new MemoryStream(data);
            image!.Source = Avalonia.Media.Imaging.Bitmap.DecodeToWidth(stream, 300);
        }

        public void SetButtonRowVisibility(bool visible) => buttonRow.IsVisible = visible;

        public void UpdateQuickAction()
        {
            quickAction.Content = post!.QuickActionName();
        }

        public async void OnQuickAction()
        {
            quickAction.Content = "Busy...";
            quickAction.IsEnabled = false;
            if (await post!.QuickAction() == ApiLinker.Generic.QuickActionUpdateType.ReloadView)
                await MainView.UpdateApiViewTask();
            else
            {
                UpdateQuickAction();
                quickAction.IsEnabled = true;
            }
        }

        public void SetInfoButtonState(bool busy)
        {
            fullView.IsEnabled = !busy;

            if (busy)
                fullView.Content = "Busy...";
            else
                fullView.Content = "Info";
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            panel = this.FindControl<DockPanel>("Panel");
            title = this.FindControl<Label>("Title");
            image = this.FindControl<Avalonia.Controls.Image>("Background");
            quickAction = this.FindControl<Button>("QuickAction");
            buttonRow = this.FindControl<StackPanel>("ButtonRow");
            fullView = this.FindControl<Button>("FullView");

            fullView.Command = new LambdaCommand(x => Dispatcher.UIThread.Post(SwitchView));
            fullView.Background = post!.Api().ApiColour().ToBrush();
            quickAction.Command = new LambdaCommand(x => Dispatcher.UIThread.Post(OnQuickAction));
            quickAction.Background = post!.Api().ApiColour().ToBrush();


            SetButtonRowVisibility(false);
            UpdateQuickAction();
            Update();
            Dispatcher.UIThread.Post(DownloadImage, DispatcherPriority.Background);
        }

        private async void SwitchView()
        {
            SetInfoButtonState(true);
            MainView.SetOverlay(new PrintPost(await post!.FullPost(), this));
        }
    }
}
