using System.IO;

namespace Backups
{
    public class JobObject
    {
        public JobObject(string path, string name)
        {
            File = new FileInfoCustom(path, name);
        }

        public FileInfoCustom File { get; }
    }
}