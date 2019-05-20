using Xamarin.Forms;

namespace demoapp
{
    public partial class webView : ContentPage
    {
        public webView()
        {
            InitializeComponent();

            var browser = new WebView();
            browser.Source = "https://www.ezystream.com";
            Content = browser;
        }
    }
}
