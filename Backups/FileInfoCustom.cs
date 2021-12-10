namespace Backups
{
    public class FileInfoCustom
    {
        public FileInfoCustom(string path, string name)
        {
            Path = path;
            Name = name;
        }

        public string Path { get; set; }
        public string Name { get; set; }
    }
}