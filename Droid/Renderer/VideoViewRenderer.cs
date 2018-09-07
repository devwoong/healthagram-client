using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using healthagram.CustomView;
using Android.App;
using Android.Views;
using Android.Widget;
using Android.Content.Res;
[assembly : ExportRenderer(typeof(CustomVideoView), typeof(healthagram.Droid.Renderer.VideoViewRenderer))]
namespace healthagram.Droid.Renderer
{
    public class VideoViewRenderer : ViewRenderer
    {
        //Create views globally so they can be referenced in OnLayout override
        VideoView videoView;
        //Android.Widget.Button playButton;
        global::Android.Views.View view;
        public VideoViewRenderer()
        {
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.View> e)
        {
            base.OnElementChanged(e);

            //  playButton.SetBackgroundColor(Android.Graphics.Color.Aqua);
            //var uri = Android.Net.Uri.Parse("https://www.dropbox.com/s/hi45psyy0wq9560/PigsInAPolka1943.mp4?dl=1");
            //videoView.SetVideoURI(uri);
            // ((CustomVideoView)Element).Uri


            var activity = Context as Activity;
            var viewHolder = activity.LayoutInflater.Inflate(Resource.Layout.SampleVideo, this, false);
            viewHolder.LayoutParameters = new LayoutParams(800, 600);
            view = viewHolder;
            AddView(view);

            //Get and set Views
            videoView = FindViewById<VideoView>(Resource.Id.SampleVideo);

           // videoView = new VideoView(Context);
            CustomVideoView video = (CustomVideoView)Element;
            Android.Net.Uri uri = Android.Net.Uri.Parse(video.Uri);
            videoView.SetVideoURI(uri);
          //  videoView.LayoutParameters = new LayoutParams(LayoutParams.MatchParent, 300);

            //videoView.SetVideoPath(video.Uri);

            ((CustomVideoView)Element).PlayVideo += PlayVideo;

            //AddView(videoView); //equal SetNativeControl(videoView);
        }
        void PlayVideo(object sender, EventArgs arg)
        {
            //Start that video!!!
            var activity = Context as Activity;
            activity.RunOnUiThread(() => {
                videoView.Start();
            });
        }
    }
}
 