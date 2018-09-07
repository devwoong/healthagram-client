using System;
using healthagram.Video;
using System.Threading.Tasks;
using Android.Content;
using Xamarin.Forms;

[assembly: Dependency(typeof(healthagram.Droid.Video.VideoPicker))]
namespace healthagram.Droid.Video
{
    public class VideoPicker : IVideoPicker
    {
        public VideoPicker()
        {
        }
        public Task<string> GetVideoPathAsync()
        {
            // Define the Intent for getting images
            Intent intent = new Intent();
            intent.SetType("video/*");
            intent.SetAction(Intent.ActionGetContent);

            // Get the MainActivity instance
            MainActivity activity = Forms.Context as MainActivity;

            // Start the picture-picker activity (resumes in MainActivity.cs)
            activity.StartActivityForResult(
                Intent.CreateChooser(intent, "Select Picture"),
                MainActivity.PickVideoId);

            // Save the TaskCompletionSource object as a MainActivity property
            activity.PickVideoTaskCompletionPath = new TaskCompletionSource<string>();

            // Return Task object
            return activity.PickVideoTaskCompletionPath.Task;
        }

    }
}
