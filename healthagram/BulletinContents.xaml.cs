using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace healthagram
{
    public partial class BulletinContents : ContentView
    {
        public string Title{
            set { title.Text = value;}
        }
        public string Date {
            set { date.Text = value;}
        }
        public string Pofile {
            set { profile.Source = ImageSource.FromUri(new Uri(value));}
        }
        public string Author {
            set { author.Text = value; }
        }
        public string UserID
        {
            set; get;
        }
        public void AddContents(View addView)
        {
            Device.BeginInvokeOnMainThread(()=> {
                content.Children.Add(addView);
            });
        }
        public void AddContentsList(IList<View> addViews)
        {
            foreach(View view in addViews)
            {
                //content.Children.Add(view);
                AddContents(view);
            }
        }
        public void RemoveContents(View removeView)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                content.Children.Remove(removeView);
            });
        }
        public IList<View> GetContentsList()
        {
            return content.Children;
        }
        public BulletinContents()
        {
            InitializeComponent();
            content = this.FindByName<StackLayout>("contents");
            this.date = this.FindByName<Label>("date");
            //date.Text = DateTime.Now.ToString();
        }
        public BulletinContents(string title, string date, string profileUri, string author, IList<View> contents)
        {
            InitializeComponent();
            content = this.FindByName<StackLayout>("content");
            profile = this.FindByName<Image>("profile");
            this.date = this.FindByName<Label>("date");
            this.author = this.FindByName<Label>("author");
            this.Author = author;
            this.Title = title;
            this.Date = date;
            //this.Pofile = profileUri;
            AddContentsList(contents);
        }
    }
}
