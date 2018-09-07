using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.IO;
using Xamarin.Forms;
namespace healthagram.Utility
{
    public class AppXmlReader : IXmlReader
    {
        private string Path = null;
        private string Descendant = null;

        public AppXmlReader()
        {
        }
        public void SetFile(string path)
        {
            Path = path;
        }
        public void SetDescendant(string descendant)
        {
            Descendant = descendant;
        }
        public string Read(string attribute)
        {
            var type = this.GetType();
            var resource = "healthagram."+ Device.RuntimePlatform +".Values." + Path;
            var stream = this.GetType().Assembly.GetManifestResourceStream(resource);

            try{
                using (var reader = new StreamReader(stream))
                {
                    var doc = XDocument.Parse(reader.ReadToEnd());
                    return doc.Element(Descendant).Element("local").Element(attribute).Value;
                } 
            }catch(Exception)
            {
                return null;
            }
        }
        public string Read(string descendant, string attribute)
        {
            string result = null;
            //Task.Factory.StartNew(delegate
            //{
            //    XDocument doc = XDocument.Load(Path);
            //    IEnumerable<string> results = from s in doc.Descendants(descendant) select s.Attribute(attribute).Value;
            //    if (results != null || results.Count() < 2)
            //    {
            //        result = results.ElementAt(0);
            //    }
            //    else result = null;
            //}).Wait();
            return result;
        }
        public string Read(string path, string descendant, string attribute)
        {
            string result = null;
            //Task.Factory.StartNew(delegate
            //{
            //    XDocument doc = XDocument.Load(path);
            //    IEnumerable<string> results = from s in doc.Descendants(descendant) select s.Attribute("Title").Value;
            //    if (results != null || results.Count() < 2)
            //    {
            //        result = results.ElementAt(0);
            //    }
            //    else result = null;
            //}).Wait();
            return result;
        }
    }
}
