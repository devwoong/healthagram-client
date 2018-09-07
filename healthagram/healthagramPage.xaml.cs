using Xamarin.Forms;
using System;
using healthagram.Utility.common;
namespace healthagram
{
    public partial class healthagramPage : ContentPage
    {
        public healthagramPage()
        {
            InitializeComponent();
            AppConfigValues.SetupValue();
            Button login = this.FindByName<Button>("login");
            login.Clicked += (sender, e) => {
                
            };
            Image google = this.FindByName<Image>("google");
            var tappedGoogle = new TapGestureRecognizer();
            tappedGoogle.Tapped += (sender, e) => {
                
            };
            google.GestureRecognizers.Add(tappedGoogle);

            Image facebook = this.FindByName<Image>("facebook");
            var tappedFacebook = new TapGestureRecognizer();
            tappedFacebook.Tapped += (sender, e) => {
                
            };
            facebook.GestureRecognizers.Add(tappedFacebook);


            //Navigation.PushAsync(new MainTab());
        }
        async void OnActionLoginClicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Question?", "게시하시겠습니까?", "Yes", "No");
            if (answer)
            {
              

            }
        }
    }
}
