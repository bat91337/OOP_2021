using System;
using System.Collections.Generic;
using System.IO;
using Backups;

namespace BackupsExtra
{
    public class LoggerFile : ILogger
    {
        public void Notify(string path, RestorePoint restorePoint)
        {
            var streamWriter = new StreamWriter(path);
            streamWriter.WriteLine($"Restore Point was created{restorePoint.Id}");
            streamWriter.WriteLine("BackupJob was Created");
        }
    }
}