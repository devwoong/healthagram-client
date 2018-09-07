using System;
using Xamarin.Forms;
using healthagram.CustomView;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using healthagram.Server;
using healthagram.Utility.common;
using healthagram.Trans.Bulletin;
namespace healthagram.Trans
{
    public class ReciveBulletinUnpacker
    {
        JArray _Contents = null;
        public ReciveBulletinUnpacker()
        {
        }
        public bool ReciveFromServer(string id, string page)
        {
            try{

                ReciveFromServerJson recive = new ReciveFromServerJson();
                string path = AppConfigValues.BulletinRegistUrl + "/" + id + "/" + page;
                _Contents = recive.Recive(path).GetValue("contents").Value<JArray>();
                if (_Contents == null)
                    return false;   
            }catch(Exception)
            {
                return false;
            }
            return true;
        }
        public IList<BulletinContents> GetContentsViews()
        {
            IList<BulletinContents> Bulletins = new List<BulletinContents>();
            foreach(JObject contents in _Contents)
            {
                IList<View> Views = new List<View>();
                SortedDictionary<int, View > sortViews = new SortedDictionary<int, View>();
                JArray Editors = contents.GetValue("editors").Value<JArray>();
                JArray Images = contents.GetValue("images").Value<JArray>();
                JArray Videos = contents.GetValue("videos").Value<JArray>();
                string Title = contents.GetValue("title").Value<JValue>().Value<string>();
                string Date = contents.GetValue("date").Value<JValue>().Value<string>();
                string author = contents.GetValue("author").Value<JValue>().Value<string>();
                string user_id = contents.GetValue("user_id").Value<JValue>().Value<string>();
                foreach (JObject editor in Editors)
                {
                    Label label = new Label();
                    label.Text = editor.GetValue("text").Value<string>();
                    int index = editor.GetValue("index").Value<int>();
                    sortViews.Add(index, label);
                   // Views.Add(label);
                }
                foreach (JObject image in Images)
                {
                    Image imageView = new Image();
                    imageView.Source = ImageSource.FromUri( new Uri(image.GetValue("path").Value<string>()) );
                    int index = image.GetValue("index").Value<int>();
                    sortViews.Add(index, imageView);
                    //Views.Add(imageView);
                }
                foreach (JObject video in Videos)
                {
                    CustomVideoView videoView = new CustomVideoView();
                    videoView.Uri = video.GetValue("path").Value<string>();
                    int index = video.GetValue("index").Value<int>();
                    sortViews.Add(index, videoView);
                    //Views.Add(videoView);
                }
                foreach(KeyValuePair<int, View> view in sortViews)
                {
                    Views.Add(view.Value);
                }

                Bulletins.Add(new BulletinContents(Title, Date, null, author, Views));
            }
            return Bulletins;
        }
    }
}
