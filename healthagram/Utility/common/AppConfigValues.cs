using System;
namespace healthagram.Utility.common
{

    public struct _Config
    {
        public string ServerUrl;
        public string BulletinRegistUrl;
        public string ImageUploadUrl;
        public string BulletinImageUrl;
    }
    public struct _User
    {
        public string UserID;
        public string Name;
    }

    static public class AppConfigValues
    {
        static _Config config;
        static public string ServerUrl{ get { return config.ServerUrl; }}
        static public string BulletinRegistUrl { get { return config.ServerUrl + config.BulletinRegistUrl; } }
        static public string ImageUploadUrl { get { return config.ServerUrl + config.ImageUploadUrl; }}
        static public string BulletinImageUrl { get { return config.ServerUrl + config.BulletinImageUrl; } }
        static public void SetupValue()
        {
            config = new _Config();
            AppConfigXmlReader reader = new AppConfigXmlReader();
            reader.SetUpConfig(ref config);
        }
    }
}
