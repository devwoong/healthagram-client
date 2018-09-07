using System;
using Xamarin.Forms;
using healthagram.CustomView;
[assembly : ExportRenderer(typeof(CustomVideoView), typeof(healthagram.iOS.Renderer.VideoVIewRenderer))]
namespace healthagram.iOS.Renderer
{
    public class VideoVIewRenderer
    {
        
        public VideoVIewRenderer()
        {
        }
    }
}
