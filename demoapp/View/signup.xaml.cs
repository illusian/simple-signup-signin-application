using System;
using demoapp.ViewModel;
using Xamarin.Forms;

namespace demoapp
{
    public partial class signup : ContentPage
    {
        public signup()
        {
            InitializeComponent();

            BindingContext = new UserViewModel();
            MessagingCenter.Subscribe<string, string>("app", "signup", async (sender, arg) => {
                if (arg == "messages.registerSuccess")
                {
                    await DisplayAlert("Sign up", "Sign up successful", "ok");
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Error", arg, "ok");
                }

            });
           
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            (Application.Current.MainPage as NavigationPage).BarBackgroundColor = Color.OrangeRed;
        }

        private void radioButton_Clicked(object sender, EventArgs e)
        {
            Image image = (Image)sender;
            string imageString = image.ClassId;

            if (imageString=="m")
            {
                radioButton1.Source = "on.png";
                radioButton2.Source = "off.png";
            }
            else
            {
                radioButton1.Source = "off.png";
                radioButton2.Source = "on.png";
            }
        }

        private void checkb_Clicked(object sender, EventArgs e)
        {
            Image image = (Image)sender;
            if ((image.Source as FileImageSource).File != "checked.png")
            {
                image.Source = "checked.png";
                image.ClassId = "1";
            }
        }

        async void openTnc(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new webView());
        }

    }
}
