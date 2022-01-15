using System;
using System.Collections.Generic;

namespace Backups
{
    internal class Program
    {
        private static void Main()
        {
            var repository = new VirtualRepository();
            var backupManager = new BackupManager();
            var jobObject = new JobObject(@"../../../Files/FileA", "FileA");
            List<JobObject> jobObjects = backupManager.AddJobObject(@"../../../Files/FileB", "FileA");
            jobObjects.Add(jobObject);
            IAlgorithm single = new SingleAlgorithm();
            DateTime dateTime = DateTime.Now;
            backupManager.CreateBackup(single, @"../../../BackupFiles/Single", jobObjects, repository, dateTime);
        }
    }
}
