using System;
using Xamarin.Forms;
namespace healthagram.CustomView
{
    public class PlaceHolderEditor : Editor
    {
        public static readonly BindableProperty PlaceholderProperty
        = BindableProperty.Create("Placeholder", typeof(string), typeof(PlaceHolderEditor), "insert content");

        public static readonly BindableProperty PlaceholderColorProperty
        = BindableProperty.Create("PlaceholderColor", typeof(Color), typeof(PlaceHolderEditor), Color.Gray);

        public static readonly BindableProperty IndexProperty
        = BindableProperty.Create("Index", typeof(int), typeof(PlaceHolderEditor), 0);


        public string Placeholder
        {
            get { return (string)this.GetValue(PlaceholderProperty); }
            set { this.SetValue(PlaceholderProperty, value); }
        }

        public Color PlaceholderColor
        {
            get { return (Color)this.GetValue(PlaceholderColorProperty); }
            set { this.SetValue(PlaceholderColorProperty, value); }
        }
        public int Index
        {
            get { return (int)this.GetValue(IndexProperty); }
            set {this.SetValue(IndexProperty, value);}
        }

        public PlaceHolderEditor()
        {
            this.TextChanged += (sender, e) => { 

                var method = typeof(View).GetMethod("InvalidateMeasure", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);

                method.Invoke(this, null);
            };
        }
        public bool IsEmpty()
        {
            if ( this.Text == null || this.Text == "" || Placeholder == this.Text)
            {
                return true;
            }
            return false;
        }
    }
}
