using System;
using System.Threading.Tasks;
using demoapp.ViewModel;
using Xamarin.Forms;

namespace demoapp
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = new UserViewModel();

            NavigationPage.SetBackButtonTitle(this, "");

            MessagingCenter.Subscribe<string, string>("app", "login", async (sender, arg) => {
                if (arg=="OK")
                {
                    await openMainAsync();
                }
                else
                {
                    await DisplayAlert("Error", "Credential error", "ok");
                }

            });
        }

        private async Task openMainAsync()
        {
            Navigation.InsertPageBefore(new MainPage(), this);
            await Navigation.PopAsync();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            (Application.Current.MainPage as NavigationPage).BarBackgroundColor = Color.FromHex("#2d2532");
        }

        async void OnSignUpButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new signup());
        }

        async void openMain(object sender, EventArgs e)
        {
            App.IsUserLoggedIn = true;
            Navigation.InsertPageBefore(new MainPage(), this);
            await Navigation.PopAsync();
        }
    }
}
