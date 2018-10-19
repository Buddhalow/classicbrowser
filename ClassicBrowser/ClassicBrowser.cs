using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
    }
}
