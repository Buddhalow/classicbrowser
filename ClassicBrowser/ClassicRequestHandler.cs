using CefSharp;
using CefSharp.Handler;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ClassicBrowser
{
    
    public class ClassicRequestHandler : DefaultRequestHandler
    {
        public ClassicRequestHandler() : base() {
        }
        
        public override bool OnBeforeBrowse(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, bool userGesture, bool isRedirect)
        {
            if (!isRedirect)
            {
                RegistryKey rkey = Registry.CurrentUser;
                RegistryKey rkey1 = rkey.OpenSubKey(@"AppEvents\Schemes\Apps\Explorer\Navigating\.Current");

                SystemSounds.PlaySound(rkey1.GetValue("").ToString());
            }
            return base.OnBeforeBrowse(browserControl, browser, frame, request, userGesture, isRedirect);
        }
    }
}
