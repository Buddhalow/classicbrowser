using CefSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassicBrowser
{
    public class ClassicBrowserLifeSpanHandler : ILifeSpanHandler
    {
        public event Action<string> PopupRequest;

        public bool OnBeforePopup(IWebBrowser browser, string sourceUrl, string targetUrl, ref int x, ref int y, ref int width,
            ref int height)
        {
            if (PopupRequest != null)
                new ClassicBrowser(targetUrl).Show();

            return true;
        }

        public void OnBeforeClose(IWebBrowser browser)
        {

        }

        public bool OnBeforePopup(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, string targetUrl, string targetFrameName, WindowOpenDisposition targetDisposition, bool userGesture, IPopupFeatures popupFeatures, IWindowInfo windowInfo, IBrowserSettings browserSettings, ref bool noJavascriptAccess, out IWebBrowser newBrowser)
        {
            ClassicBrowser classicBrowser = new ClassicBrowser(targetUrl);
            classicBrowser.Show();
            newBrowser = classicBrowser.WebBrowser;
            return true;
        }

        public void OnAfterCreated(IWebBrowser chromiumWebBrowser, IBrowser browser)
        {
        }

        public bool DoClose(IWebBrowser chromiumWebBrowser, IBrowser browser)
        {
            return false;
        }

        public void OnBeforeClose(IWebBrowser chromiumWebBrowser, IBrowser browser)
        {
        }
    }
}
