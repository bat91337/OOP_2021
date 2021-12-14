using System;
using System.Collections.Generic;
using System.IO;
using Backups;

namespace BackupsExtra
{
    public class LoggerFile : ILogger
    {
        public void Notify()
        {
            var streamWriter = new StreamWriter("../../../text.txt");
            streamWriter.WriteLine("Restore Point was created");
            streamWriter.WriteLine("BackupJob was Created");
        }
    }
}