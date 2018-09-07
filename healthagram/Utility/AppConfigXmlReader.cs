using System;
using healthagram.Utility.common;
namespace healthagram.Utility
{
    public class AppConfigXmlReader : AppXmlReader
    {
        public AppConfigXmlReader()
        {
        }
        public void SetUpConfig(ref _Config config)
        {
            SetFile("AppValues.xml");
            SetDescendant("Server");
            config.ServerUrl = this.Read("url");
            config.ImageUploadUrl = this.Read("ImageUpload");
            config.BulletinRegistUrl = this.Read("BulletinReg");
            config.BulletinImageUrl = this.Read("BulletinImage");
        }
    }
}
