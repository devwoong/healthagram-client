using System;
using Xamarin.Forms;
using UIKit;
using Foundation;
using System.Threading.Tasks;
using healthagram.Video;
using System.IO;

[assembly: Dependency(typeof(healthagram.iOS.Video.VideoPicker))]
namespace healthagram.iOS.Video
{
    public class VideoPicker : IVideoPicker
    {
        UIImagePickerController Picker = new UIImagePickerController();

        TaskCompletionSource<string> taskCompletionSource;
        UIImagePickerController imagePicker;

        public Task<string> GetVideoPathAsync()
        {
            // Create and define UIImagePickerController
            imagePicker = new UIImagePickerController
            {
                SourceType = UIImagePickerControllerSourceType.PhotoLibrary,
                MediaTypes = UIImagePickerController.AvailableMediaTypes(UIImagePickerControllerSourceType.PhotoLibrary)
            };

            // Set event handlers
            imagePicker.FinishedPickingMedia += OnVideoPickerFinishedPickingMedia;
            imagePicker.Canceled += OnImagePickerCancelled;

            // Present UIImagePickerController;
            UIWindow window = UIApplication.SharedApplication.KeyWindow;
            var viewController = window.RootViewController;
            viewController.PresentModalViewController(imagePicker, true);

            // Return Task object
            taskCompletionSource = new TaskCompletionSource<string>();
            return taskCompletionSource.Task;
        }


        void OnVideoPickerFinishedPickingMedia(object sender, UIImagePickerMediaPickedEventArgs args)
        {
            NSUrl Media = args.MediaUrl;

            if ( Media != null)
            {
                
                // Set the Stream as the completion of the Task
                taskCompletionSource.SetResult(Media.Path);
            }
            else
            {
                taskCompletionSource.SetResult(null);
            }
            imagePicker.DismissModalViewController(true);
        }

        void OnImagePickerCancelled(object sender, EventArgs args)
        {
            taskCompletionSource.SetResult(null);
            imagePicker.DismissModalViewController(true);
        }


        protected void Handle_FinishedPickingMedia(object sender, UIImagePickerMediaPickedEventArgs e)
        {
            // determine what was selected, video or image
            bool isImage = false;
            switch (e.Info[UIImagePickerController.MediaType].ToString())
            {
                case "public.image":
                    Console.WriteLine("Image selected");
                    isImage = true;
                    break;
                case "public.video":
                    Console.WriteLine("Video selected");
                    break;
            }

            // get common info (shared between images and video)
            NSUrl referenceURL = e.Info[new NSString("UIImagePickerControllerReferenceURL")] as NSUrl;
            if (referenceURL != null)
                Console.WriteLine("Url:" + referenceURL.ToString());

            // if it was an image, get the other image info
            if (isImage)
            {
                // get the original image
                UIImage originalImage = e.Info[UIImagePickerController.OriginalImage] as UIImage;
                if (originalImage != null)
                {
                    // do something with the image
                    Console.WriteLine("got the original image");
                    //imageView.Image = originalImage; // display
                }
            }
            else
            { // if it's a video
              // get video url
                NSUrl mediaURL = e.Info[UIImagePickerController.MediaURL] as NSUrl;
                if (mediaURL != null)
                {
                    Console.WriteLine(mediaURL.ToString());
                }
            }
            // dismiss the picker
            Picker.DismissViewControllerAsync(true);
            //Picker.DismissModalViewControllerAnimated(true);
        }
        void Handle_Canceled(object sender, EventArgs e)
        {
            Picker.DismissViewControllerAsync(true);
            //Picker.DismissModalViewControllerAnimated(true);
        }
    }
}
