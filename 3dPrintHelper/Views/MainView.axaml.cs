using ApiLinker;
using ApiLinker.Interfaces;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using System.Collections.Generic;
using System.Linq;
using _3dPrintHelper.Service;
using System.Threading.Tasks;
using System.Windows.Input;
using System;
using _3dPrintHelper.ViewsExt;
using Avalonia.Media;

namespace _3dPrintHelper.Views
{
    public partial class MainView : UserControlExt<MainView>
    {
        public MainWindow Window { get; }
        private ListBox list;
        private StackPanel topBar;
        private MenuItem sitesMenu;
        private API api;
        private Label apiNameLabel;
        private Label apiSortType;
        private Label loadingLabel;
        private UserControl overlay;
        private Panel overlayPanel;
        private Button leftArrow;
        private Button rightArrow;
        private Label pageNum;
        private TextBox searchBox;
        private Button searchButton;
        
        [NamedControl]
        public ComboBox PerPageSelection { get; set; }

        [Binding(nameof(PerPageSelection), "Items")]
        public List<string> PageSelectionItems => new() {"10", "20", "30", "40", "50"};

        [Binding(nameof(PerPageSelection), "SelectedIndex")]
        public int PageSelectionItemsIndex => (perPage - 10) / 10;

        private string currentSortType;
        private int perPage = 20;
        private int page = 1;
        private IApi currentApi;
        private PrintPostSmall? LastSelected = null;

        public MainView(MainWindow window)
        {
            api = new API();
            currentApi = api.APIs[0];
            currentSortType = currentApi.SortTypes()[0];
            this.Window = window;
            InitializeComponent();
            SetControls();
            InitialiseData();
            UpdateView();
        }

        public MainView()
            : this(null) { }


        private bool lastUpdateTypeSearch = false;
        public async Task UpdateApiViewTask(bool search = false)
        {
            LastSelected = null;
            leftArrow.IsEnabled = false;
            rightArrow.IsEnabled = false;
            list.Items = new List<PrintPostSmall>();
            loadingLabel.Content = "Loading...";

            List<IPreviewPost> posts;

            if (search)
            {
                posts = await currentApi.GetPostsBySearch(searchBox.Text, perPage, (page - 1) * perPage);
                apiSortType.Content = "Search";
                lastUpdateTypeSearch = true;
            }
            else
            {
                posts = await currentApi.GetPosts(currentSortType, perPage, (page - 1) * perPage);
                apiSortType.Content = currentSortType;
                lastUpdateTypeSearch = false;
            }

            apiNameLabel.Content = currentApi.ApiName();
            topBar.Background = currentApi.ApiColour().ToBrush();
            List<PrintPostSmall> tempList = posts.Select(x => new PrintPostSmall(x, this)).ToList();
            list.Items = tempList;
            if (tempList.Count <= 0)
                loadingLabel.Content = "No items found";

            long totalCount = currentApi.GetItemCountOnLastRequest();
            if (totalCount < 0)
            {
                pageNum.Content = $"Page {page}";
                rightArrow.IsEnabled = true;
            }
            else
            {
                long totalPages = (totalCount + perPage - 1) / perPage;
                pageNum.Content = $"Page {page}/{totalPages}";
                rightArrow.IsEnabled = (page < totalPages);
            }

            leftArrow.IsEnabled = (page > 1);
        }

        public async void UpdateApiView() => await UpdateApiViewTask(false);
        public async void UpdateApiViewSearch() => await UpdateApiViewTask(true);
        private async void UpdateApiViewLast() => await UpdateApiViewTask(lastUpdateTypeSearch);

        public void SetOverlay(object? target = null)
        {
            overlay.Content = target;
            overlayPanel.IsVisible = (target != null);
        }

        private void OnMenuChange(IApi api, string sortType)
        {
            currentSortType = sortType;
            page = 1;
            currentApi = api;

            Dispatcher.UIThread.Post(UpdateApiView);
        }

        private void OnPageLeft()
        {
            page--;
            Dispatcher.UIThread.Post(UpdateApiViewLast);
        }

        private void OnPageRight()
        {
            page++;
            Dispatcher.UIThread.Post(UpdateApiViewLast);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            list = this.FindControl<ListBox>("List");
            topBar = this.FindControl<StackPanel>("TopBar");
            sitesMenu = this.FindControl<MenuItem>("SitesMenu");
            apiNameLabel = this.FindControl<Label>("ApiNameLabel");
            apiSortType = this.FindControl<Label>("ApiSortType");
            loadingLabel = this.FindControl<Label>("LoadingLabel");
            overlay = this.FindControl<UserControl>("Overlay");
            overlayPanel = this.FindControl<Panel>("OverlayPanel");
            overlayPanel.Background = new SolidColorBrush(new Color(128, 0, 0, 0));
            leftArrow = this.FindControl<Button>("LeftArrow");
            rightArrow = this.FindControl<Button>("RightArrow");
            pageNum = this.FindControl<Label>("PageNum");
            searchBox = this.FindControl<TextBox>("SearchBox");
            searchButton = this.FindControl<Button>("SearchButton");

            list.SelectionChanged += List_SelectionChanged;

            List<MenuItem> menuItems = new();

            api.APIs.ForEach(x =>
            {
                MenuItem rootItem = new();
                rootItem.Header = x.ApiName();
                menuItems.Add(rootItem);

                rootItem.Items = x.SortTypes().Select(y =>
                {
                    MenuItem subItem = new();
                    subItem.Header = y;
                    subItem.Command = new LambdaCommand(z => OnMenuChange(x, y));
                    return subItem;
                }).ToList();
            });

            sitesMenu.Items = menuItems;

            Dispatcher.UIThread.Post(UpdateApiView);

            leftArrow.Command = new LambdaCommand(x => OnPageLeft());
            rightArrow.Command = new LambdaCommand(x => OnPageRight());
            searchButton.Command = new LambdaCommand(x =>
            {
                page = 1;
                Dispatcher.UIThread.Post(UpdateApiViewSearch);
            });
        }

        private void InitialiseData()
        {
            PerPageSelection.SelectionChanged += (sender, args) =>
            {
                perPage = (PerPageSelection.SelectedIndex + 1) * 10;
                page = 1;
                Dispatcher.UIThread.Post(UpdateApiView);
            };
        }
        
        private void List_SelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            LastSelected?.SetButtonRowVisibility(false);
            LastSelected = list.SelectedItem as PrintPostSmall;
            LastSelected?.SetButtonRowVisibility(true);
        }
    }
}
