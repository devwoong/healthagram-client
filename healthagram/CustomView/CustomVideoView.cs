using System;
using Xamarin.Forms;
namespace healthagram.CustomView
{
    public enum VIDEO
    {
        Video_uri,
        Video_Path,
    }
    public class CustomVideoView : View
    {
        public static readonly BindableProperty UriProperty
        = BindableProperty.Create("Uri", typeof(string), typeof(CustomVideoView));
        public static readonly BindableProperty VideoSourceTypeProperty
        = BindableProperty.Create("VideoSourceType", typeof(VIDEO), typeof(CustomVideoView), VIDEO.Video_Path);
        public event EventHandler PlayVideo;
        public string Uri
        {
            get { return (string)GetValue(UriProperty); }
            set { SetValue(UriProperty,value); }
        }
        public VIDEO VideoSourceType
        {
            get { return (VIDEO)GetValue(VideoSourceTypeProperty); }
            set { SetValue(VideoSourceTypeProperty, value); }
        }
        public void Play()
        {
            if (PlayVideo != null)
                PlayVideo(this, EventArgs.Empty);
        }
    }
}
