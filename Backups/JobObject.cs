using System.IO;

namespace Backups
{
    public class JobObject
    {
        public JobObject(string path)
        {
            File = new FileInfo(path);
        }

        public FileInfo File { get; set; }
    }
}