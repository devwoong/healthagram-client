using System;
using System.Collections.Generic;

using Xamarin.Forms;
using healthagram.ImageLoader;
using System.IO;
using System.Threading.Tasks;
using healthagram.CustomView;
using healthagram.Video;
using healthagram.Trans.Bulletin;
using healthagram.Trans;
using healthagram.Utility.common;
namespace healthagram
{
    public partial class BulletinEditPage : ContentPage
    {
        StackLayout Contents;
        int ContentIndex = 0;
        Bulletin bulletin;
        public BulletinEditPage()
        {
            InitializeComponent();
            bulletin = new Bulletin();
            Contents = this.FindByName<StackLayout>("content");
            Picker noticeScope = this.FindByName<Picker>("noticeScope");
            noticeScope.Items.Add("공개");
            noticeScope.Items.Add("비공개");
            noticeScope.Items.Add("친구공개");
            noticeScope.Items.Add("제외한 친구 공개");
            noticeScope.Items.Add("선택한 친구 공개");


            Image pictureImage = this.FindByName<Image>("picture");
            var tapPictureImage = new TapGestureRecognizer();
            tapPictureImage.Tapped += (sender, e) => {
                Image addImage = new Image();
                Task<Stream> PickImage = DependencyService.Get<IImagePicker>().GetImageStreamAsync();
                PickImage.GetAwaiter().OnCompleted( () => {
                    addImage = new Image();
                    Stream stream = PickImage.Result;

                    byte[] imageAsBytes;
                    using (var memoryStream = new MemoryStream())
                    {
                        stream.CopyTo(memoryStream);
                        imageAsBytes = memoryStream.ToArray();
                    }
                    Server.FIleUploader uploader = new Server.FIleUploader();
                    string fileurl = uploader.UploadFilesToRemoteUrl(AppConfigValues.ImageUploadUrl + "/" + "test", imageAsBytes);
                    addImage.Source = ImageSource.FromUri( new Uri(fileurl) );
                    bulletin.AddImage(fileurl, ContentIndex++);
                    //addImage.Source = ImageSource.FromStream(() => stream);
                    this.Optimization();
                    PlaceHolderEditor editor = new PlaceHolderEditor();
                    editor.Index = ContentIndex++;
                    Contents.Children.Add(addImage);
                    Contents.Children.Add(editor);
                });
            };
            pictureImage.GestureRecognizers.Add( tapPictureImage);

            Image videoImage = this.FindByName<Image>("video");
            var tapVideoImage = new TapGestureRecognizer();
            tapVideoImage.Tapped += (sender, e) => {
                Task<string> PickVideo = DependencyService.Get<IVideoPicker>().GetVideoPathAsync();
                PickVideo.GetAwaiter().OnCompleted(() =>
                {
                    CustomVideoView videoView = new CustomVideoView();
                    videoView.Uri = PickVideo.Result;
                    this.Optimization();
                    PlaceHolderEditor editor = new PlaceHolderEditor();
                    editor.Index = ContentIndex++;
                    Contents.Children.Add(videoView);
                    Contents.Children.Add(editor);
                    videoView.Play();

                });
            };
            videoImage.GestureRecognizers.Add(tapVideoImage);

            PlaceHolderEditor initialText = new PlaceHolderEditor();
            initialText.Index = ContentIndex++;
            Contents.Children.Add(initialText);
            //   content.Children.Add(entry);

            Button submit = this.FindByName<Button>("Done");
            submit.Clicked += OnActionSubmitClicked;

        }
        private void Optimization()
        {
            foreach( View View in Contents.Children )
            {
                if( View is Editor)
                {
                    if(((PlaceHolderEditor)View).IsEmpty())
                    {
                        //Task.Run(async () => {
                        //    await DeleteView(video);
                        //});
                        Device.BeginInvokeOnMainThread(() => {
                            Contents.Children.Remove(View);
                        });
                    }
                }
            }
        }
        async void OnActionSubmitClicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Question?", "게시하시겠습니까?", "Yes", "No");
            if(answer)
            {
                Optimization();
                foreach (View View in Contents.Children)
                {
                    if (View is PlaceHolderEditor)
                    {
                        PlaceHolderEditor editor = ((PlaceHolderEditor)View);
                        bulletin.AddTextEditor(editor.Text, editor.Index);
                    }
                }
                //Bulletin.Pofile = this.FindByName<Entry>("profile").Text;

                BulletinInfo info = new BulletinInfo();
                info.date = DateTime.Now.ToString();
                info.isAnonym = this.FindByName<Switch>("isAnonym").IsToggled;
                info.title = this.FindByName<Entry>("title").Text;
                info.noticeScope = this.FindByName<Picker>("noticeScope").SelectedItem.ToString();
                info.author = "test_name";
                info.user_id = "test";
                bulletin.SetInfo(info);
                SendBulletinPacker Packer = new SendBulletinPacker(bulletin);
                if(Packer.SendContent())
                {
                    await this.Navigation.PushAsync(new MainTab());
                }

            }
        }
    }
}
