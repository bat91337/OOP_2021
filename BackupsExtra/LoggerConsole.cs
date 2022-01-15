using System;
using System.Collections.Generic;
using Backups;

namespace BackupsExtra
{
    public class LoggerConsole : ILogger
    {
        public void Notify(string path, RestorePoint restorePoint)
        {
            Console.WriteLine($"Restore Point was created{restorePoint.Id}");
            Console.WriteLine("Storage was Created");
        }
    }
}