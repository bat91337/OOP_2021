using System;
using System.Collections.Generic;
using Backups;

namespace BackupsExtra
{
    public class LoggerConsole : ILogger
    {
        public void Notify()
        {
            Console.WriteLine("Restore Point was created");
            Console.WriteLine("BackupJob was Created");
        }
    }
}