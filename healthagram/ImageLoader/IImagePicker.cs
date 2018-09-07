using System;
using Xamarin.Forms;
using System.IO;
using System.Threading.Tasks;
namespace healthagram.ImageLoader
{
    public interface IImagePicker
    {
        Task<Stream> GetImageStreamAsync();
    }
}
