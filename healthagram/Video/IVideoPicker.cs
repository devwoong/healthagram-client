using System;
using System.Threading.Tasks;
namespace healthagram.Video
{
    public interface IVideoPicker
    {
        Task<string> GetVideoPathAsync();
    }
}
