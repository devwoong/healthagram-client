using System;
namespace healthagram.Utility
{
    public interface IXmlReader
    {
        void SetFile(string path);
        void SetDescendant(string desendant);
        string Read(string descendant, string attribute);
        string Read(string attribute);
        string Read(string path, string descendant, string attribute);
    }
}
