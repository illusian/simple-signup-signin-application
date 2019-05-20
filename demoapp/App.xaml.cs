using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace demoapp
{
    public partial class App : Application
    {
        public static bool IsUserLoggedIn { get; set; }

        public App()
        {
            InitializeComponent();

            //MainPage = new MainPage();
            if (!IsUserLoggedIn)
            {
                var loginPage = new NavigationPage(new LoginPage());
                this.MainPage = loginPage;

                loginPage.BarBackgroundColor = Color.FromHex("2d2532");
                loginPage.BarTextColor = Color.White;
            }
            else
            {
                MainPage = new MainPage();
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
