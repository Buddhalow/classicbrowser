using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassicBrowser
{
    public partial class ClassicBrowser : Form
    {

        public ClassicBrowser()
        {
            InitializeComponent();
            this.Resize += Form1_Resize;
            Init();
            Pack();
        }
        public string StartUrl
        {
            get
            {
                String startPage = Properties.Settings.Default.StartPage;
                if (String.IsNullOrEmpty(startPage))
                {
                    startPage = "http://www.google.se";
                }
                return startPage;
            }
        }
        private void Init()
        {

            
            webBrowser = new ChromiumWebBrowser(StartUrl);
            webBrowser.TitleChanged += WebBrowser_TitleChanged;
            webBrowser.AddressChanged += WebBrowser_AddressChanged;
            this.toolStripContainer1.ContentPanel.Controls.Add(webBrowser);
            webBrowser.Dock = DockStyle.Fill;
            webBrowser.LoadingStateChanged += WebBrowser_LoadingStateChanged;
            webBrowser.FrameLoadStart += WebBrowser_FrameLoadStart;
            webBrowser.LifeSpanHandler = new ClassicBrowserLifeSpanHandler();

            LoadFavorites();
            RenderFavoriteMenus();
        }

        private void WebBrowser_FrameLoadStart(object sender, CefSharp.FrameLoadStartEventArgs e)
        {
           
        }

        private void WebBrowser_LoadingStateChanged(object sender, CefSharp.LoadingStateChangedEventArgs e)
        {
        }

        private void WebBrowser_AddressChanged(object sender, CefSharp.AddressChangedEventArgs e)
        {
            Invoke((MethodInvoker)delegate
            {
                AddressBar.Text = e.Address;

            });
        }

        private void WebBrowser_TitleChanged(object sender, CefSharp.TitleChangedEventArgs e)
        {
            Invoke((MethodInvoker)delegate
            {
                Text = e.Title + " - Web Browser Provided by Buddhalow";
                CurrentTitle = e.Title;
            });
        }

        private ChromiumWebBrowser webBrowser;
        public ClassicBrowser(String url) : this()
        {
            webBrowser.Load(url);
        }

        private void WebBrowser1_DocumentTitleChanged(object sender, EventArgs e)
        {
        }

        public ChromiumWebBrowser WebBrowser
        {
            get
            {
                return webBrowser;
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            Pack();
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            webBrowser.Load(AddressBar.Text);
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            webBrowser.GetBrowser().Reload();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            webBrowser.GetBrowser().StopLoad();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            webBrowser.GetBrowser().GoForward();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            webBrowser.GetBrowser().GoBack();
        }

        public FavoriteManager Favorites
        {
            get
            {
                return FavoriteManager.FavoritesManager;
            }
        }
     
        public string FavoritesFile
        {
            get
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Favorites.json";

            }
        }

        private void SaveFavorites()
        {
            FavoriteManager.Save();
        }

        private void LoadFavorites()
        {
            FavoriteManager.Load();
        }
        private void RenderFavoriteMenu(ToolStripDropDownItem item)
        {
            
            item.DropDownItems.Clear();
            ToolStripItem addToFavoritesItem = item.DropDownItems.Add("Add to favorites");
            addToFavoritesItem.Click += AddToFavoritesItem_Click;
            ToolStripItem organizeFavoritesItem = item.DropDownItems.Add("Organize favorites");
            organizeFavoritesItem.Click += OrganizeFavoritesItem_Click;
            item.DropDownItems.Add("-");
            foreach(Favorite favorite in FavoriteManager.FavoritesManager.Favorites)
            {
                ToolStripItem favoriteItem = item.DropDownItems.Add(favorite.Name);
                favoriteItem.Tag = favorite;
                favoriteItem.Click += FavoriteItem_Click;
            }
        }

        private void OrganizeFavoritesItem_Click(object sender, EventArgs e)
        {
            ManageFavoritesForm manageFavoritesForm = new ManageFavoritesForm();
            manageFavoritesForm.ShowDialog();
            RenderFavoriteMenus();
        }

        private void RenderFavoriteToolStrip(ToolStrip item)
        {

            item.Items.Clear();
            ToolStripLabel favoritesToolStripLabel = new ToolStripLabel("Favorites");
            favoritesToolStripLabel.Font = new Font(favoritesToolStripLabel.Font, FontStyle.Bold);
            item.Items.Add(favoritesToolStripLabel);
           
            foreach (Favorite favorite in FavoriteManager.FavoritesManager.Favorites)
            {
                ToolStripItem favoriteItem = item.Items.Add(favorite.Name);
                favoriteItem.Tag = favorite;
                favoriteItem.Click += FavoriteItem_Click;
                favoriteItem.TextImageRelation = TextImageRelation.ImageBeforeText;
                favoriteItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            }
        }
        private void AddToFavoritesItem_Click(object sender, EventArgs e)
        {
            AddPageToFavorites();
        }

        private void FavoriteItem_Click(object sender, EventArgs e)
        {
            Favorite favorite = (Favorite)((ToolStripItem)sender).Tag;
            webBrowser.Load(favorite.Url);
        }

        public string CurrentTitle;

        private void AddPageToFavorites()
        {
            
            FavoriteForm favoriteForm = new FavoriteForm()
            {
                Url = AddressBar.Text, // TODO bind it to web browsers url
                Title = CurrentTitle // TODO bind to text
            };
            if (favoriteForm.ShowDialog() == DialogResult.OK) {
                Favorite favorite = favoriteForm.Favorite;
                FavoriteManager.FavoritesManager.Favorites.Add(favorite);
                SaveFavorites();
                RenderFavoriteMenus();
            }
        }
        public void RenderFavoriteMenus()
        {
            RenderFavoriteMenu(favoritesButton);
            RenderFavoriteMenu(favoritesMenu);
            RenderFavoriteToolStrip(favoritesToolStrip);
        }

        private void Pack()
        {
            this.AddressBar.Size = new Size(this.Width - this.btnGo.Width - toolStripLabel1.Width - 50, this.AddressBar.Height);
            toolStripStatusLabel2.Size = new Size(Width - 200, toolStripLabel2.Height);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void webBrowser1_MarginChanged(object sender, EventArgs e)
        {

        }

        private void webBrowser1_LocationChanged(object sender, EventArgs e)
        {
        }

        private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            Invoke((MethodInvoker)delegate
            {
                AddressBar.Text = e.Url.ToString();
            });
        }

        private void webBrowser1_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripStatusLabel2_Click(object sender, EventArgs e)
        {

        }

        private void ClassicBrowser_Shown(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked_1(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void AddressBar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                webBrowser.Load(AddressBar.Text);
                e.Handled = true;
                e.SuppressKeyPress = true;

            }
        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            webBrowser.Load("https://buddhalow.se");
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            webBrowser.Load(StartUrl);
        }

        private void backToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webBrowser.GetBrowser().GoBack();
        }

        private void forwardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webBrowser.GetBrowser().GoForward();

        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not implemented yet sorry!");
        }

        private void toolStripContainer1_ContentPanel_Load(object sender, EventArgs e)
        {

        }

        private void nyttFönsterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ClassicBrowser().Show();
        }

        private void favoritesButton_Click(object sender, EventArgs e)
        {

        }

        private void favoritesToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
