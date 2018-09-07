using System;

using System.IO;
using System.Threading.Tasks;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
namespace healthagram.Droid
{
    [Activity(Label = "healthagram.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            LoadApplication(new App());
        }

        // Field, property, and method for Picture Picker
        public static readonly int PickImageId = 1000;
        public static readonly int PickVideoId = 1001;
        public TaskCompletionSource<Stream> PickImageTaskCompletionSource { set; get; }
        public TaskCompletionSource<string> PickVideoTaskCompletionPath { set; get; }
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent intent)
        {
            base.OnActivityResult(requestCode, resultCode, intent);

            if (requestCode == PickImageId)
            {
                if ((resultCode == Result.Ok) && (intent != null))
                {
                    Android.Net.Uri uri = intent.Data;
                    Stream stream = ContentResolver.OpenInputStream(uri);

                    // Set the Stream as the completion of the Task
                    PickImageTaskCompletionSource.SetResult(stream);
                }
                else
                {
                    PickImageTaskCompletionSource.SetResult(null);
                }
            }
            else if(requestCode == PickVideoId)
            {
                if ((resultCode == Result.Ok) && (intent != null))
                {
                    Android.Net.Uri uri = intent.Data;
                    string path = intent.Data.ToString();
                    //Stream stream = ContentResolver.OpenInputStream(uri);

                    // Set the Stream as the completion of the Task
                    PickVideoTaskCompletionPath.SetResult(path);
                }
                else
                {
                    PickVideoTaskCompletionPath.SetResult(null);
                }
            }
        }
    }
}
