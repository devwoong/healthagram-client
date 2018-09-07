using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace healthagram
{
    public partial class MainTab : TabbedPage
    {
        public MainTab()
        {
            InitializeComponent();
            Children.Add(new BulletinBoadTab());
        }

    }
}
