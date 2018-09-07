using System;
using System.IO;
using System.Collections.Generic;
namespace healthagram.Trans.Bulletin
{
    public struct ImageInfo
    {
        public int index;
        public string path;
        //public byte[] source;
        //public string filename;
    }
    public struct TextInfo 
    {
        public int index;
        public string text;
    }
    public struct VideoInfo 
    {
        public int index;
        public string path;
        //public byte[] source;
        //public string filename;
    }
    public class Bulletin
    {
        BulletinInfo Info;
        IList<TextInfo> TextEditors;
        IList<ImageInfo> Images;
        IList<VideoInfo> Videos;

        public BulletinInfo GetInfo()
        {
            return Info;
        }
        public void SetInfo(BulletinInfo info) 
        {
            Info = info;
        }
        public void SetInfo(string user_id, string author, string title, string noticeScope, bool isAnonym) 
        {
            Info = new BulletinInfo();
            Info.date = DateTime.Now.ToString();
            Info.isAnonym = isAnonym;
            Info.title = title;
            Info.noticeScope = noticeScope;
            Info.author = author;
            Info.user_id = user_id;
        }
        public Bulletin()
        {
            TextEditors = new List<TextInfo>();
            Images = new List<ImageInfo>();
            Videos = new List<VideoInfo>();
        }
        public void AddTextEditor(string Text, int index)
        {
            TextInfo addText = new TextInfo();
            addText.text = Text;
            addText.index = index;
            TextEditors.Add(addText);
        }
        public void AddImage(string path, int index)
        {
            ImageInfo addImage = new ImageInfo();
            addImage.path = path;
            addImage.index = index;
            Images.Add(addImage);
        }
        public void AddVideo(string path, int index)
        {
            VideoInfo addVideo = new VideoInfo();
            addVideo.path = path;
            addVideo.index = index;
            Videos.Add(addVideo);
            
        }
        public IList<TextInfo> GetTextEditor()
        {
            return TextEditors;
        }

        public IList<ImageInfo> GetImage()
        {
            return Images;
        }

        public IList<VideoInfo> GetVideo()
        {
            return Videos;
        }
        public void RemoveEmptyElement()
        {
            foreach(TextInfo text in TextEditors )
            {
                if (text.text == null || text.text.Trim() == "")
                    TextEditors.Remove(text);
            }
        }
    }
}
