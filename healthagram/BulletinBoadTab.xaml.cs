using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Xamarin.Forms;
using healthagram.Trans;

namespace healthagram
{
    public partial class BulletinBoadTab : ContentPage
    {
        public BulletinBoadTab()
        {
            InitializeComponent();
            Title = "게시판";
            StackLayout bulletins = this.FindByName<StackLayout>("Bulletins");
            ReciveBulletinUnpacker FromServerRecive = new ReciveBulletinUnpacker();
            if (FromServerRecive.ReciveFromServer("test", "0"))
            {
                IList<BulletinContents> fromServerBulletin = FromServerRecive.GetContentsViews();
                foreach (BulletinContents bulletin in fromServerBulletin)
                {
                    bulletins.Children.Add(bulletin);
                }
            }

            Image editImage = this.FindByName<Image>("edit");
            var tapImage = new TapGestureRecognizer();  
            tapImage.Tapped += tapImage_Tapped; 
            editImage.GestureRecognizers.Add(tapImage);
        }
        public void tapImage_Tapped(object sender, EventArgs e) 
        {
            Navigation.PushAsync(new BulletinEditPage());
        }
    }
}
