using System.IO;
using System.Threading.Tasks;

namespace Logger.File
{
    public static class FileExtensions
    {
        public static Task DeleteAsync(this FileInfo fi)
        {
            return Task.Factory.StartNew(() => fi.Delete());
        }
    }
}
