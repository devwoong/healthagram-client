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
    public class SendBulletinPacker
    {
        JObject contents = null;
        public SendBulletinPacker()
        {
            
        }
        public SendBulletinPacker(Bulletin.Bulletin bulletin)
        {
            contents = new JObject();
            JArray editorArray = new JArray();
            JArray imageArray = new JArray();
            JArray videoArray = new JArray();

            foreach( TextInfo editor in bulletin.GetTextEditor() )
            {
                JObject editorObject = new JObject();

                editorObject.Add("text", editor.text);
                editorObject.Add("index", editor.index.ToString());
                editorArray.Add(editorObject);
            }
            foreach(ImageInfo image in bulletin.GetImage())
            {
                JObject ImageObject = new JObject();

                ImageObject.Add("path", image.path);
                ImageObject.Add("index", image.index.ToString());
                imageArray.Add(ImageObject);
            }
            foreach (VideoInfo video in bulletin.GetVideo())
            {
                JObject VideoObject = new JObject();
                VideoObject.Add("path", video.path);
                VideoObject.Add("index", video.index.ToString());
                videoArray.Add(VideoObject);
            }
            BulletinInfo info = bulletin.GetInfo();
            contents.Add("videos", videoArray);
            contents.Add("images", imageArray);
            contents.Add("editors", editorArray);
            contents.Add("date", info.date);
            contents.Add("author", info.author);
            contents.Add("user_id", info.user_id);
            contents.Add("title", info.title);
            contents.Add("isAnonym", info.isAnonym);
            contents.Add("notice_scope", info.noticeScope);
        }
        public bool SendContent()
        {
            SendToServerJson sendContent = new SendToServerJson();
            string contentsString = contents.ToString();
            return sendContent.Send(AppConfigValues.BulletinRegistUrl, contents.ToString());
        }
    }
}
